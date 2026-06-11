using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Application.Features.Drives.Queries;
using CertificationHub.Application.Features.Reports.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/dashboard")]
[Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
public sealed class DashboardController(IMediator mediator) : ApiControllerBase
{
    [HttpGet("overview")]
    public async Task<IActionResult> Overview(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetDashboardStatsQuery(), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("drive/{driveId:guid}")]
    public async Task<IActionResult> Drive(Guid driveId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetDriveDashboardQuery(driveId), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("funnel/{driveId:guid}")]
    public async Task<IActionResult> Funnel(Guid driveId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetDriveFunnelQuery(driveId), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("utilization")]
    public async Task<IActionResult> Utilization([FromQuery] Guid driveId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetUtilizationReportQuery(driveId), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("sla")]
    public IActionResult Sla()
    {
        return OkResponse(new
        {
            registrationAcknowledgementMinutes = 5,
            voucherDeliveryHours = 24,
            resultNotificationMinutes = 30
        });
    }
}
