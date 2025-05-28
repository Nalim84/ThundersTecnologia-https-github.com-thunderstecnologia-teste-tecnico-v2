using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Domain.Contracts;

public interface ITollRepository
{
    Task<Toll> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Toll> CreateAsync(Toll toll, CancellationToken cancellationToken = default);
}
