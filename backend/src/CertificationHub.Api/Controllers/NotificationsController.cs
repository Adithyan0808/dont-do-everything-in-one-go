using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Application.Features.Notifications.Commands;
using CertificationHub.Application.Features.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notifications")]
[Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
public sealed class NotificationsController(IMediator mediator) : ApiControllerBase
{
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetNotificationsByUserQuery(userId), cancellationToken);
        return OkResponse(response);
    }

    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> MarkRead(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new MarkNotificationReadCommand(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("unread-count/{userId:guid}")]
    public async Task<IActionResult> UnreadCount(Guid userId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetNotificationsByUserQuery(userId, true), cancellationToken);
        return OkResponse(new { count = response.Count });
    }
}
