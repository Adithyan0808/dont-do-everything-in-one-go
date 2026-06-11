using CertificationHub.Application.Features.Drives.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Drives.Queries;

public sealed record GetDriveByIdQuery(Guid DriveId) : IRequest<DriveResponseDto>;

public sealed record GetAllDrivesQuery(bool ActiveOnly = false) : IRequest<IReadOnlyList<DriveListDto>>;

public sealed record GetDriveDashboardQuery(Guid DriveId) : IRequest<DriveDashboardDto>;
