namespace CertificationHub.Api.Authorization;

public static class AuthorizationPolicies
{
    public const string AdminOnly = nameof(AdminOnly);
    public const string CoordinatorOrAdmin = nameof(CoordinatorOrAdmin);
    public const string ApproverOnly = nameof(ApproverOnly);
    public const string CandidateOnly = nameof(CandidateOnly);
    public const string AuthenticatedUser = nameof(AuthenticatedUser);
}

public static class AuthorizationRoles
{
    public const string Admin = nameof(Admin);
    public const string SuperAdmin = nameof(SuperAdmin);
    public const string Coordinator = nameof(Coordinator);
    public const string Approver = nameof(Approver);
    public const string Manager = nameof(Manager);
    public const string Employee = nameof(Employee);
    public const string Candidate = nameof(Candidate);
}
