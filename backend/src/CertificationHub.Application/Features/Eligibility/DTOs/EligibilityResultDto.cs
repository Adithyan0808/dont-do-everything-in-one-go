namespace CertificationHub.Application.Features.Eligibility.DTOs;

public sealed record RuleResultDto(string RuleName, string Result, string Message);

public sealed record EligibilityResultDto(
    string OverallResult,
    IReadOnlyList<RuleResultDto> RuleResults,
    IReadOnlyList<string> FailureReasons,
    IReadOnlyList<string> Warnings);
