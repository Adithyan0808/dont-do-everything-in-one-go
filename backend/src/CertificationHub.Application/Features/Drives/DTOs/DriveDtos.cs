using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Drives.DTOs;

public sealed record CreateDriveDto(
    string DriveName,
    string VendorName,
    string CertificationName,
    string SponsorName,
    decimal BudgetAllocated,
    bool ManagerApprovalRequired,
    int AttemptLimit,
    int TenureRequiredDays,
    DateTime RegistrationStartDate,
    DateTime RegistrationEndDate,
    DateTime ExamWindowStartDate,
    DateTime ExamWindowEndDate,
    int TargetCount,
    string PolicyUrl);

public sealed record UpdateDriveDto(
    Guid DriveId,
    string DriveName,
    string SponsorName,
    decimal BudgetAllocated,
    bool ManagerApprovalRequired,
    int AttemptLimit,
    int TenureRequiredDays,
    DateTime RegistrationStartDate,
    DateTime RegistrationEndDate,
    DateTime ExamWindowStartDate,
    DateTime ExamWindowEndDate,
    int TargetCount,
    string PolicyUrl);

public sealed record DriveResponseDto(
    Guid DriveId,
    string DriveCode,
    string DriveName,
    string VendorName,
    string CertificationName,
    string SponsorName,
    decimal BudgetAllocated,
    decimal BudgetConsumed,
    DriveStatus Status,
    DateTime RegistrationStartDate,
    DateTime RegistrationEndDate,
    DateTime ExamWindowStartDate,
    DateTime ExamWindowEndDate,
    int TargetCount,
    int CurrentRegistrationCount,
    int CurrentCertifiedCount,
    string PolicyUrl);

public sealed record DriveListDto(
    Guid DriveId,
    string DriveCode,
    string DriveName,
    string VendorName,
    string CertificationName,
    DriveStatus Status,
    DateTime RegistrationStartDate,
    DateTime RegistrationEndDate,
    int TargetCount,
    int CurrentRegistrationCount);

public sealed record DriveDashboardDto(
    Guid DriveId,
    string DriveName,
    int TargetCount,
    int CurrentRegistrationCount,
    int CurrentCertifiedCount,
    decimal BudgetAllocated,
    decimal BudgetConsumed,
    decimal CertificationRate);
