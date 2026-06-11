namespace CertificationHub.Application.Rules;

public sealed class PriorAttemptsRule : IEligibilityRule
{
    public string Name => nameof(PriorAttemptsRule);

    public Task<EligibilityRuleResult> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default)
    {
        var passed = context.Registration.PriorAttempts < context.Drive.AttemptLimit;
        return Task.FromResult(new EligibilityRuleResult(
            Name,
            passed ? EligibilityRuleStatus.Passed : EligibilityRuleStatus.Failed,
            passed ? "Attempt limit check passed." : "Attempt limit exceeded."));
    }
}
