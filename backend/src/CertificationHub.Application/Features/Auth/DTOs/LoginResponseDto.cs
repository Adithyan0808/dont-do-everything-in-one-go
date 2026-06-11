namespace CertificationHub.Application.Features.Auth.DTOs;

public sealed record LoginResponseDto(string AccessToken, CurrentUserDto User);
