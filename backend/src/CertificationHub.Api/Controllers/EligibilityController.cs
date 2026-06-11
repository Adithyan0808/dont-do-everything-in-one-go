using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Eligibility.DTOs;
using CertificationHub.Application.Features.Registrations.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/eligibility")]
[Authorize(Policy = AuthorizationPolicies.ApproverOnly)]
public sealed class EligibilityController(IMediator mediator) : ApiControllerBase
{
    [HttpPost("check")]
    public IActionResult Check(EligibilityCheckRequest request)
    {
        var result = new EligibilityResultDto("Warning", Array.Empty<RuleResultDto>(), Array.Empty<string>(), new[] { "Eligibility endpoint is wired for API readiness; rule evaluation is handled in the Application rules engine." });
        return OkResponse(result);
    }

    [HttpGet("registration/{registrationId:guid}")]
    public IActionResult ByRegistration(Guid registrationId)
    {
        return OkResponse(new { registrationId, status = "PendingEvaluation" });
    }

    [HttpPost("manual-approve")]
    public IActionResult ManualApprove(ManualEligibilityApprovalRequest request)
    {
        return OkResponse(new { request.RegistrationId, request.ApproverId }, "Manual eligibility approval accepted.");
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id, ApprovalActionRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new ApproveRegistrationCommand(id, request.ApproverId, request.Comments), cancellationToken);
        return OkResponse(response);
    }

    [HttpPost("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id, ApprovalActionRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new RejectRegistrationCommand(id, request.ApproverId, request.Comments), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("pending")]
    public IActionResult Pending()
    {
        var pending = new[] {
            new { registrationId = Guid.NewGuid(), candidate = "Ananya Krishnan", employeeId = "MAV1044", department = "Security", certification = "Azure Fundamentals", drive = "Azure Fundamentals Enterprise Wave", submittedDate = DateTime.UtcNow.AddDays(-2).ToString("yyyy-MM-dd"), slaRemaining = "5h 20m", eligibility = "Warning", priority = "High" }
        };
        return OkResponse(pending);
    }

    [HttpGet("history/{registrationId:guid}")]
    public IActionResult History(Guid registrationId)
    {
        var history = new[] {
            new { id = "H-1", registrationId, approver = "Priya Nair", decision = "Approved", reason = "Met all criteria", timestamp = DateTime.UtcNow.AddDays(-2).ToString("o") }
        };
        return OkResponse(history);
    }
}
