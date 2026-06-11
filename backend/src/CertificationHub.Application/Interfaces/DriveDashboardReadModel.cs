namespace CertificationHub.Application.Interfaces;

public sealed record DriveDashboardReadModel(
    Guid DriveId,
    string DriveName,
    int TargetCount,
    int CurrentRegistrationCount,
    int CurrentCertifiedCount,
    decimal BudgetAllocated,
    decimal BudgetConsumed);
