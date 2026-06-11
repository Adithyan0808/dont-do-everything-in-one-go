namespace CertificationHub.Application.Rules;

public sealed class ManagerApprovalRule : IEligibilityRule
{
    public string Name => nameof(ManagerApprovalRule);

    public Task<EligibilityRuleResult> EvaluateAsync(EligibilityContext context, CancellationToken cancellationToken = default)
    {
        var approved = !context.Drive.ManagerApprovalRequired || context.Registration.ApproverId.HasValue;
        return Task.FromResult(new EligibilityRuleResult(
            Name,
            approved ? EligibilityRuleStatus.Passed : EligibilityRuleStatus.Failed,
            approved ? "Manager approval requirement satisfied." : "Manager approval is required."));
    }
}
