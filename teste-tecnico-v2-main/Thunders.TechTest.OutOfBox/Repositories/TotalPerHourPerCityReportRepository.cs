using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Contexts;

namespace Thunders.TechTest.OutOfBox.Repositories;

public class TotalPerHourPerCityReportRepository : ITotalPerHourPerCityReportRepository
{

    private readonly DefaultContext _context;
    private readonly IReportRespository _reportRespository;

    public TotalPerHourPerCityReportRepository(DefaultContext context, IReportRespository reportRespository)
    {
        _context = context;
        _reportRespository = reportRespository;
    }

    public async Task<Report> CreateReportTransaction(Report report, ICollection<TotalPerHourPerCityReport> totalPerHourPerCityReports, CancellationToken cancellationToken = default)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _reportRespository.CreateReport(report, cancellationToken);

            await _context.AddRangeAsync(totalPerHourPerCityReports);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return report;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ICollection<TotalPerHourPerCityReport>> GetTotalPerHourPerCityReport(Guid reportId, string cityName, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _context.TollTransactions
       .Include(x => x.Toll)
       .Where(x => x.Toll.City == cityName)
       .GroupBy(x => x.CreatedAt.Hour)
       .Select(g => new TotalPerHourPerCityReport
       {
           Id = Guid.NewGuid(),
           ReportId = reportId,
           CityName = cityName,
           Hour = g.Key,
           TotalPerHour = g.Sum(x => x.AmountPaid),
           CreatedAt = DateTimeOffset.Now
       })
       .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            var messageaaa = ex.Message;
            //_logger.LogError(ex, "Erro ao salvar alterações no banco de dados.");
            throw;
        }

    }
}
