using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CertificationHub.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CertificationHub.Application.Services;

public sealed class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
{
    public string GenerateAccessToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = jwtSettings["Key"] ?? "development-only-secret-key-change-in-next-steps";
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("EmployeeId", user.EmployeeId),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FullName", user.FullName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.TryParse(jwtSettings["AccessTokenMinutes"], out var minutes) ? minutes : 60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
