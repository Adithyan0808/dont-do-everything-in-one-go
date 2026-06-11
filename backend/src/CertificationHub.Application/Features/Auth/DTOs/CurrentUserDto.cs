using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Auth.DTOs;

public sealed record CurrentUserDto(
    Guid UserId,
    string EmployeeId,
    string FullName,
    string Email,
    UserRole Role,
    string Department,
    string BusinessUnit);
