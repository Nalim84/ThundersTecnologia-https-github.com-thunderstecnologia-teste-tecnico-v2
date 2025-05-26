using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Domain.Contracts;

public interface ITollTransactionRepository
{
    Task<TollTransaction> CreateAsync(TollTransaction tollTransaction, CancellationToken cancellationToken = default);
}
