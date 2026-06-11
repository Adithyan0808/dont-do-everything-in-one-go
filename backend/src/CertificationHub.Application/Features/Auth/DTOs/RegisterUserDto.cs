using CertificationHub.Domain.Enums;

namespace CertificationHub.Application.Features.Auth.DTOs;

public sealed record RegisterUserDto(
    string EmployeeId,
    string FullName,
    string Email,
    string Department,
    string BusinessUnit,
    string Location,
    string ManagerEmail,
    UserRole Role,
    string Password);
