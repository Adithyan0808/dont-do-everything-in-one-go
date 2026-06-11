namespace CertificationHub.Application.Features.Reports.DTOs;

public sealed record DashboardStatsDto(int ActiveDrives, int TotalRegistrations, int CertifiedCount, int AvailableVouchers);

public sealed record DriveFunnelDto(Guid DriveId, string DriveName, int Registered, int Approved, int VoucherAssigned, int Certified);

public sealed record UtilizationReportDto(Guid DriveId, string DriveName, decimal BudgetAllocated, decimal BudgetConsumed, int VoucherCount);

public sealed record CertificationTrendDto(string CertificationName, int Count, int Year, int Month);

public sealed record DepartmentPerformanceDto(string Department, int Registrations, int Certifications);
