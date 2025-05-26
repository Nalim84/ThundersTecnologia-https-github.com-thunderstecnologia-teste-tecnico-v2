using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Thunders.TechTest.OutOfBox.Repositories
{
    public class TollRepository : ITollRepository
    {

        private readonly DefaultContext _context;

        public TollRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Toll in the database
        /// </summary>
        /// <param name="toll">The toll to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created user</returns>
        public async Task<Toll> CreateAsync(Toll toll, CancellationToken cancellationToken = default)
        {
            await _context.Tolls.AddAsync(toll, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return toll;
        }

        public async Task<Toll> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tolls.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
    }
}
