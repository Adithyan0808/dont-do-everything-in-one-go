using Asp.Versioning;
using CertificationHub.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[Route("api/v{version:apiVersion}/health")]
[ApiVersion("1.0")]
public sealed class HealthController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return OkResponse(new { status = "Healthy", service = "Maverick Certification Hub API" });
    }
}
