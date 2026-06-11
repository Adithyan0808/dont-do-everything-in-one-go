using CertificationHub.Application.Features.Auth.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Auth.Queries;

public sealed record GetCurrentUserQuery(Guid UserId) : IRequest<CurrentUserDto>;
