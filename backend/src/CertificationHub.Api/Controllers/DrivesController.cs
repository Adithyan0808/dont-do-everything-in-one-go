using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Drives.Commands;
using CertificationHub.Application.Features.Drives.DTOs;
using CertificationHub.Application.Features.Drives.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/drives")]
[Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
public sealed class DrivesController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public async Task<IActionResult> Create(CreateDriveDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateDriveCommand(request), cancellationToken);
        return CreatedResponse($"/api/v1/drives/{response.DriveId}", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllDrivesQuery(), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllDrivesQuery(true), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("archived")]
    public async Task<IActionResult> GetArchived(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllDrivesQuery(), cancellationToken);
        return OkResponse(response.Where(drive => drive.Status.ToString().Equals("Cancelled", StringComparison.OrdinalIgnoreCase)).ToArray());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetDriveByIdQuery(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public async Task<IActionResult> Update(Guid id, UpdateDriveDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateDriveCommand(request with { DriveId = id }), cancellationToken);
        return OkResponse(response);
    }

    [HttpPut("{id:guid}/status")]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public async Task<IActionResult> UpdateStatus(Guid id, DriveStatusRequest request, CancellationToken cancellationToken)
    {
        var response = request.Status.ToLowerInvariant() switch
        {
            "active" or "open" => await mediator.Send(new ActivateDriveCommand(id), cancellationToken),
            "closed" => await mediator.Send(new CloseDriveCommand(id), cancellationToken),
            "archived" or "cancelled" => await mediator.Send(new ArchiveDriveCommand(id), cancellationToken),
            _ => throw new CertificationHub.Application.Exceptions.BusinessRuleException("Unsupported drive status.")
        };

        return OkResponse(response);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ArchiveDriveCommand(id), cancellationToken);
        return OkResponse(response, "Drive archived successfully.");
    }

    [HttpGet("{id:guid}/dashboard")]
    public async Task<IActionResult> Dashboard(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetDriveDashboardQuery(id), cancellationToken);
        return OkResponse(response);
    }
}
