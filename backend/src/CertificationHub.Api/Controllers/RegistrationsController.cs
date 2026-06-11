using Asp.Versioning;
using System.Security.Claims;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Registrations.Commands;
using CertificationHub.Application.Features.Registrations.DTOs;
using CertificationHub.Application.Features.Registrations.Queries;
using CertificationHub.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/registrations")]
[Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
public sealed class RegistrationsController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(CreateRegistrationDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RegisterForDriveCommand(request), cancellationToken);
        return CreatedResponse($"/api/v1/registrations/{response.RegistrationId}", response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetRegistrationByIdQuery(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        if (!CanAccessUserRegistrations(userId))
        {
            return Forbid();
        }

        var response = await mediator.Send(new GetUserRegistrationsQuery(userId), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("drive/{driveId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public async Task<IActionResult> GetByDrive(Guid driveId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetRegistrationsByDriveQuery(driveId), cancellationToken);
        return OkResponse(response);
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public async Task<IActionResult> UpdateStatus(Guid id, DriveStatusRequest request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<RegistrationStatus>(request.Status, true, out var status))
        {
            throw new CertificationHub.Application.Exceptions.BusinessRuleException("Unsupported registration status.");
        }

        var response = await mediator.Send(new UpdateRegistrationStatusCommand(id, status), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/schedule")]
    public async Task<IActionResult> Schedule(Guid id, ScheduleExamRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ScheduleExamCommand(id, request.PreferredSlot), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/approve")]
    [Authorize(Policy = AuthorizationPolicies.ApproverOnly)]
    public async Task<IActionResult> Approve(Guid id, ApprovalActionRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ApproveRegistrationCommand(id, request.ApproverId, request.Comments), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/reject")]
    [Authorize(Policy = AuthorizationPolicies.ApproverOnly)]
    public async Task<IActionResult> Reject(Guid id, ApprovalActionRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RejectRegistrationCommand(id, request.ApproverId, request.Comments), cancellationToken);
        return OkResponse(response);
    }

    private bool CanAccessUserRegistrations(Guid requestedUserId)
    {
        var currentUserId = User.FindFirstValue("UserId");
        var isOwnRegistration = Guid.TryParse(currentUserId, out var parsedUserId) && parsedUserId == requestedUserId;
        var isPrivileged = User.IsInRole(AuthorizationRoles.Admin)
            || User.IsInRole(AuthorizationRoles.SuperAdmin)
            || User.IsInRole(AuthorizationRoles.Coordinator)
            || User.IsInRole(AuthorizationRoles.Approver)
            || User.IsInRole(AuthorizationRoles.Manager);

        return isOwnRegistration || isPrivileged;
    }
}
