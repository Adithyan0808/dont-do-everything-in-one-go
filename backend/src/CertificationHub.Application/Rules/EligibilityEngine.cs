using CertificationHub.Application.Features.Eligibility.DTOs;

namespace CertificationHub.Application.Rules;

public sealed class EligibilityEngine(IEnumerable<IEligibilityRule> rules)
{
    public async Task<EligibilityResultDto> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default)
    {
        var results = new List<EligibilityRuleResult>();

        foreach (var rule in rules)
        {
            results.Add(await rule.EvaluateAsync(context, cancellationToken));
        }

        var failures = results
            .Where(result => result.Status == EligibilityRuleStatus.Failed)
            .Select(result => result.Message)
            .ToArray();
        var warnings = results
            .Where(result => result.Status == EligibilityRuleStatus.Warning)
            .Select(result => result.Message)
            .ToArray();

        return new EligibilityResultDto(
            failures.Length == 0 ? "Passed" : "Failed",
            results.Select(result => new RuleResultDto(result.RuleName, result.Status.ToString(), result.Message)).ToArray(),
            failures,
            warnings);
    }
}
