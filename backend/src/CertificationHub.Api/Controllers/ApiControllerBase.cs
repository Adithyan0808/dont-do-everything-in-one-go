using CertificationHub.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected string CorrelationId => HttpContext.Items["CorrelationId"]?.ToString() ?? HttpContext.TraceIdentifier;

    protected OkObjectResult OkResponse<T>(T data, string message = "Operation completed successfully")
        => Ok(ApiResponse<T>.Ok(data, message, CorrelationId));

    protected OkObjectResult OkResponse(string message = "Operation completed successfully")
        => Ok(ApiResponse.Ok(message, CorrelationId));

    protected CreatedResult CreatedResponse<T>(string uri, T data, string message = "Resource created successfully")
        => Created(uri, ApiResponse<T>.Ok(data, message, CorrelationId));
}
