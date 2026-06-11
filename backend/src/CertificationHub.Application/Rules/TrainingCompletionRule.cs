namespace CertificationHub.Application.Rules;

public sealed class TrainingCompletionRule : IEligibilityRule
{
    public string Name => nameof(TrainingCompletionRule);

    public Task<EligibilityRuleResult> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new EligibilityRuleResult(
            Name,
            context.TrainingCompleted ? EligibilityRuleStatus.Passed : EligibilityRuleStatus.Warning,
            context.TrainingCompleted ? "Required training completed." : "Training completion is pending."));
    }
}
