using CertificationHub.Domain.Enums;

namespace CertificationHub.Api.Models;

public sealed record DriveStatusRequest(string Status);

public sealed record ScheduleExamRequest(string PreferredSlot);

public sealed record ApprovalActionRequest(Guid ApproverId, string Comments);

public sealed record ManualEligibilityApprovalRequest(Guid RegistrationId, Guid ApproverId, string Remarks);

public sealed record SendReminderRequest(Guid DriveId, string Message);

public sealed record FileImportRequest(string FileName);

public sealed record EligibilityCheckRequest(Guid RegistrationId, int TenureDays, bool TrainingCompleted, decimal AvailableBudget);
