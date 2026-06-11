namespace CertificationHub.Application.Rules;

public sealed class TenureEligibilityRule : IEligibilityRule
{
    public string Name => nameof(TenureEligibilityRule);

    public Task<EligibilityRuleResult> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default)
    {
        var passed = context.TenureDays >= context.Drive.TenureRequiredDays;
        return Task.FromResult(new EligibilityRuleResult(
            Name,
            passed ? EligibilityRuleStatus.Passed : EligibilityRuleStatus.Failed,
            passed ? "Tenure requirement satisfied." : "Tenure requirement not satisfied."));
    }
}
