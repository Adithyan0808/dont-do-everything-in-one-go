namespace CertificationHub.Application.Rules;

public sealed class BudgetAvailabilityRule : IEligibilityRule
{
    public string Name => nameof(BudgetAvailabilityRule);

    public Task<EligibilityRuleResult> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default)
    {
        var passed = context.AvailableBudget > 0 && context.Drive.BudgetAllocated >= context.Drive.BudgetConsumed;
        return Task.FromResult(new EligibilityRuleResult(
            Name,
            passed ? EligibilityRuleStatus.Passed : EligibilityRuleStatus.Warning,
            passed ? "Budget is available." : "Budget availability requires review."));
    }
}
