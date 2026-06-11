using CertificationHub.Application.Features.Auth.DTOs;
using CertificationHub.Domain.Enums;
using MediatR;

namespace CertificationHub.Application.Features.Auth.Commands;

public sealed record RegisterUserCommand(
    string EmployeeId,
    string FullName,
    string Email,
    string Department,
    string BusinessUnit,
    string Location,
    string ManagerEmail,
    UserRole Role,
    string Password) : IRequest<CurrentUserDto>;
