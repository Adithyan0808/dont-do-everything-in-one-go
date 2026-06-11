namespace CertificationHub.Application.Rules;

public interface IEligibilityRule
{
    string Name { get; }

    Task<EligibilityRuleResult> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default);
}

public sealed record EligibilityRuleResult(string RuleName, EligibilityRuleStatus Status, string Message);

public enum EligibilityRuleStatus
{
    Passed = 1,
    Failed = 2,
    Warning = 3
}
