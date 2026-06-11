using System.Text;
using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/audit")]
[Authorize(Policy = AuthorizationPolicies.AdminOnly)]
public sealed class AuditController(IAuditService auditService) : ApiControllerBase
{
    [HttpGet("logs")]
    public IActionResult Logs()
    {
        return OkResponse(Array.Empty<object>(), "Use entity audit endpoint for targeted audit trails.");
    }

    [HttpGet("entity/{entityName}/{entityId}")]
    public async Task<IActionResult> Entity(string entityName, string entityId, CancellationToken cancellationToken)
    {
        var response = await auditService.GetAuditTrailAsync(entityName, entityId, cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("export")]
    public IActionResult Export([FromQuery] string format = "csv")
    {
        var content = "Timestamp,EntityName,EntityId,Action,ActorName";
        var bytes = Encoding.UTF8.GetBytes(content);
        var contentType = format.Equals("excel", StringComparison.OrdinalIgnoreCase)
            ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            : "text/csv";

        return File(bytes, contentType, $"audit-export.{(format.Equals("excel", StringComparison.OrdinalIgnoreCase) ? "xlsx" : "csv")}");
    }
}
