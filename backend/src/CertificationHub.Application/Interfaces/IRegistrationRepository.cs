using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Interfaces;

public interface IRegistrationRepository : IGenericRepository<Registration>
{
    Task<IReadOnlyList<Registration>> GetPendingApprovalsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Registration>> GetUserRegistrationsAsync(Guid userId, CancellationToken cancellationToken = default);
}
