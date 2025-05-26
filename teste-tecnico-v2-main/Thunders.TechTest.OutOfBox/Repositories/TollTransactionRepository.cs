using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Contexts;

namespace Thunders.TechTest.OutOfBox.Repositories;

public class TollTransactionRepository : ITollTransactionRepository
{
    private readonly DefaultContext _context;

    public TollTransactionRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Toll Transaction in the database
    /// </summary>
    /// <param name="tollTransaction">The toll to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<TollTransaction> CreateAsync(TollTransaction tollTransaction, CancellationToken cancellationToken = default)
    {
        await _context.TollTransactions.AddAsync(tollTransaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return tollTransaction;
    }
}
