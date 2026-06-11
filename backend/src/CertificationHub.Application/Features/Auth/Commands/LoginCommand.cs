using CertificationHub.Application.Features.Auth.DTOs;
using MediatR;

namespace CertificationHub.Application.Features.Auth.Commands;

public sealed record LoginCommand(string Email, string Password) : IRequest<LoginResponseDto>;
