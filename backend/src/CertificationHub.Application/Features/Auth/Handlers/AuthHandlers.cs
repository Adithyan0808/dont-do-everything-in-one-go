using AutoMapper;
using CertificationHub.Application.Exceptions;
using CertificationHub.Application.Features.Auth.Commands;
using CertificationHub.Application.Features.Auth.DTOs;
using CertificationHub.Application.Features.Auth.Queries;
using CertificationHub.Application.Interfaces;
using CertificationHub.Application.Services;
using CertificationHub.Domain.Entities;
using MediatR;

namespace CertificationHub.Application.Features.Auth.Handlers;

public sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IApplicationUnitOfWork unitOfWork,
    IMapper mapper,
    IAuditService auditService) : IRequestHandler<RegisterUserCommand, CurrentUserDto>
{
    public async Task<CurrentUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetByEmployeeIdAsync(request.EmployeeId, cancellationToken) is not null)
        {
            throw new BusinessRuleException("Employee ID is already registered.");
        }

        if (await userRepository.GetByEmailAsync(request.Email, cancellationToken) is not null)
        {
            throw new BusinessRuleException("Email is already registered.");
        }

        var user = mapper.Map<User>(request);
        user.UserId = Guid.NewGuid();
        user.IsActive = true;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await auditService.LogActionAsync(
            nameof(User),
            user.UserId.ToString(),
            "Registration",
            user.UserId,
            user.FullName,
            "{}",
            $"{{\"email\":\"{user.Email}\",\"role\":\"{user.Role}\"}}",
            cancellationToken: cancellationToken);

        return mapper.Map<CurrentUserDto>(user);
    }
}

public sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IMapper mapper,
    IAuditService auditService) : IRequestHandler<LoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            await auditService.LogActionAsync(
                nameof(User),
                request.Email,
                "Login Failure",
                Guid.Empty,
                request.Email,
                "{}",
                "{\"reason\":\"Invalid credentials\"}",
                cancellationToken: cancellationToken);
            throw new UnauthorizedException("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            await auditService.LogActionAsync(
                nameof(User),
                user.UserId.ToString(),
                "Login Failure",
                user.UserId,
                user.FullName,
                "{}",
                "{\"reason\":\"Account disabled\"}",
                cancellationToken: cancellationToken);
            throw new UnauthorizedException("User account is inactive.");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            await auditService.LogActionAsync(
                nameof(User),
                user.UserId.ToString(),
                "Login Failure",
                user.UserId,
                user.FullName,
                "{}",
                "{\"reason\":\"Invalid credentials\"}",
                cancellationToken: cancellationToken);
            throw new UnauthorizedException("Invalid email or password.");
        }

        await auditService.LogActionAsync(
            nameof(User),
            user.UserId.ToString(),
            "Login Success",
            user.UserId,
            user.FullName,
            "{}",
            "{\"status\":\"Success\"}",
            cancellationToken: cancellationToken);

        return new LoginResponseDto(jwtTokenGenerator.GenerateAccessToken(user), mapper.Map<CurrentUserDto>(user));
    }
}

public sealed class GetCurrentUserQueryHandler(
    IUserRepository userRepository,
    IMapper mapper) : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
{
    public async Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.UserId);

        return mapper.Map<CurrentUserDto>(user);
    }
}
