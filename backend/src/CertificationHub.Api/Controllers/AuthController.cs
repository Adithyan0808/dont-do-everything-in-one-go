using System.Security.Claims;
using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Auth.Commands;
using CertificationHub.Application.Features.Auth.DTOs;
using CertificationHub.Application.Features.Auth.Queries;
using CertificationHub.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public sealed class AuthController(IMediator mediator, IAuditService auditService) : ApiControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("LoginPolicy")]
    public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new LoginCommand(request.Email, request.Password), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("register")]
    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    public async Task<IActionResult> Register(RegisterUserDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RegisterUserCommand(
            request.EmployeeId,
            request.FullName,
            request.Email,
            request.Department,
            request.BusinessUnit,
            request.Location,
            request.ManagerEmail,
            request.Role,
            request.Password), cancellationToken);

        return CreatedResponse($"/api/v1/auth/users/{response.UserId}", response);
    }

    [HttpGet("me")]
    [Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue("UserId") ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return Unauthorized(ApiResponse.Fail("Invalid user claims.", new[] { "UserId claim is missing." }, CorrelationId));
        }

        var response = await mediator.Send(new GetCurrentUserQuery(parsedUserId), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("refresh-token")]
    [Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
    public IActionResult RefreshToken()
    {
        return OkResponse(new { refreshTokenSupported = false }, "Refresh token endpoint is future ready.");
    }

    [HttpPost("logout")]
    [Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue("UserId");
        var actorId = Guid.TryParse(userId, out var parsedUserId) ? parsedUserId : Guid.Empty;
        await auditService.LogActionAsync(
            "Session",
            actorId.ToString(),
            "Logout",
            actorId,
            User.FindFirstValue("FullName") ?? User.Identity?.Name ?? "unknown",
            "{}",
            "{\"status\":\"LoggedOut\"}",
            cancellationToken: cancellationToken);
        return OkResponse("Logout completed.");
    }
}
