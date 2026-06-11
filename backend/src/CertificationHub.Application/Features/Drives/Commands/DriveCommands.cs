using CertificationHub.Application.Features.Drives.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Drives.Commands;

public sealed record CreateDriveCommand(CreateDriveDto Drive) : IRequest<DriveResponseDto>;

public sealed record UpdateDriveCommand(UpdateDriveDto Drive) : IRequest<DriveResponseDto>;

public sealed record ActivateDriveCommand(Guid DriveId) : IRequest<DriveResponseDto>;

public sealed record CloseDriveCommand(Guid DriveId) : IRequest<DriveResponseDto>;

public sealed record ArchiveDriveCommand(Guid DriveId) : IRequest<DriveResponseDto>;
