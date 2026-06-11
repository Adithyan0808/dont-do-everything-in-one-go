using Asp.Versioning;
using CertificationHub.Api.Authorization;
using CertificationHub.Api.Models;
using CertificationHub.Application.Features.Assessments.Commands;
using CertificationHub.Application.Features.Assessments.DTOs;
using CertificationHub.Application.Features.Assessments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificationHub.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/assessments")]
[Authorize(Policy = AuthorizationPolicies.AuthenticatedUser)]
public sealed class AssessmentsController(IMediator mediator) : ApiControllerBase
{
    private const long MaxUploadBytes = 5 * 1024 * 1024;
    private static readonly string[] AllowedExtensions = new[] { ".csv", ".xlsx", ".xls" };

    [HttpPost("submit")]
    public async Task<IActionResult> Submit(SubmitAssessmentDto request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new SubmitAssessmentCommand(request), cancellationToken);
        return CreatedResponse($"/api/v1/assessments/{response.ResultId}", response);
    }

    [HttpPost("import")]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public IActionResult Import(IFormFile file)
    {
        if (file.Length > MaxUploadBytes)
        {
            return BadRequest(ApiResponse.Fail("File too large.", new[] { "Maximum file size is 5 MB." }, CorrelationId));
        }

        var extension = Path.GetExtension(file.FileName);
        if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
        {
            return BadRequest(ApiResponse.Fail("Invalid file format.", new[] { "Only CSV and Excel files are supported." }, CorrelationId));
        }

        return OkResponse(new { file.FileName, file.Length }, "Assessment import accepted for processing.");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAssessmentByIdQuery(id), cancellationToken);
        return OkResponse(response);
    }

    [HttpGet("drive/{driveId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.CoordinatorOrAdmin)]
    public async Task<IActionResult> GetByDrive(Guid driveId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAssessmentsByDriveQuery(driveId), cancellationToken);
        return OkResponse(response);
    }
}
