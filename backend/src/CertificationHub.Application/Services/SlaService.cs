using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Services;

public sealed class SlaService : ISlaService
{
    public SLAStatus EvaluateRegistrationAcknowledgement(DateTime registeredDate, DateTime acknowledgedDate)
        => acknowledgedDate - registeredDate <= TimeSpan.FromMinutes(5) ? SLAStatus.OnTrack : SLAStatus.Breached;

    public SLAStatus EvaluateVoucherDelivery(DateTime assignedDate, DateTime? deliveryDate)
        => deliveryDate is null
            ? SLAStatus.AtRisk
            : deliveryDate.Value - assignedDate <= TimeSpan.FromHours(24) ? SLAStatus.OnTrack : SLAStatus.Breached;

    public SLAStatus EvaluateResultNotification(DateTime submissionDate, DateTime notificationDate)
        => notificationDate - submissionDate <= TimeSpan.FromMinutes(30) ? SLAStatus.OnTrack : SLAStatus.Breached;
}
