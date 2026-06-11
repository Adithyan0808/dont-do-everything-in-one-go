using CertificationHub.Application.Interfaces;
using CertificationHub.Domain.Entities;
using CertificationHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.EmployeeId == employeeId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }
}
