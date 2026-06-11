using CertificationHub.Application.Features.Reports.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Reports.Queries;

public sealed record GetDashboardStatsQuery : IRequest<DashboardStatsDto>;

public sealed record GetDriveFunnelQuery(Guid DriveId) : IRequest<DriveFunnelDto>;

public sealed record GetUtilizationReportQuery(Guid DriveId) : IRequest<UtilizationReportDto>;

public sealed record GetCertificationTrendQuery : IRequest<IReadOnlyList<CertificationTrendDto>>;
