using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Services;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
}
