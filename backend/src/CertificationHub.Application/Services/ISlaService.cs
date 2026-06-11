using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Services;

public interface ISlaService
{
    SLAStatus EvaluateRegistrationAcknowledgement(DateTime registeredDate, DateTime acknowledgedDate);

    SLAStatus EvaluateVoucherDelivery(DateTime assignedDate, DateTime? deliveryDate);

    SLAStatus EvaluateResultNotification(DateTime submissionDate, DateTime notificationDate);
}
