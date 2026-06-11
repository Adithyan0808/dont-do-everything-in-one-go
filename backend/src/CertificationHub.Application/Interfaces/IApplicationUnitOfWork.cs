namespace CertificationHub.Application.Interfaces;

public interface IApplicationUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
