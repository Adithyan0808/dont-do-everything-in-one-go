using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Vouchers.Commands;
using CertificationHub.Application.Features.Vouchers.DTOs;
using CertificationHub.Application.Features.Vouchers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/vouchers")]
[Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
public sealed class VouchersController(IMediator mediator) : ApiControllerBase
{
    [HttpPost("pool")]
    public async Task<IActionResult> CreatePool(CreateVoucherPoolDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateVoucherPoolCommand(request), cancellationToken);
        return CreatedResponse("/api/v1/vouchers/pool", response);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign(AssignVoucherDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new AssignVoucherCommand(request), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/deliver")]
    public async Task<IActionResult> Deliver(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeliverVoucherCommand(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/revoke")]
    public async Task<IActionResult> Revoke(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RevokeVoucherCommand(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/reissue")]
    public async Task<IActionResult> Reissue(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ReissueVoucherCommand(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("drive/{driveId:guid}")]
    public async Task<IActionResult> GetByDrive(Guid driveId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetDriveVouchersQuery(driveId), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("{id:guid}/status")]
    public async Task<IActionResult> Status(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetVoucherByIdQuery(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("send-reminders")]
    public IActionResult SendReminders(SendReminderRequest request)
    {
        return OkResponse(new { request.DriveId }, "Voucher reminders accepted for processing.");
    }
}
