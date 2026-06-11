using CertificationHub.Application.Features.Registrations.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Registrations.Queries;

public sealed record GetRegistrationByIdQuery(Guid RegistrationId) : IRequest<RegistrationResponseDto>;

public sealed record GetRegistrationsByDriveQuery(Guid DriveId) : IRequest<IReadOnlyList<RegistrationSummaryDto>>;

public sealed record GetUserRegistrationsQuery(Guid UserId) : IRequest<IReadOnlyList<RegistrationSummaryDto>>;
