using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Rules;

public sealed record EligibilityContext(
    User User,
    CertificationDrive Drive,
    Registration Registration,
    int TenureDays,
    bool TrainingCompleted,
    decimal AvailableBudget);
