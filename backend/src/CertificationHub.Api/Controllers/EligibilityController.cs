using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Eligibility.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/eligibility")]
[Authorize(Policy = AuthorizationPolicies.ApproverOnly)]
public sealed class EligibilityController : ApiControllerBase
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
}
