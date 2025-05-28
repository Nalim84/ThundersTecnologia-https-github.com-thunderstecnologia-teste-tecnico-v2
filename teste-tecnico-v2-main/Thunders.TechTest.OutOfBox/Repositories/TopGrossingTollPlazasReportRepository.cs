using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Contexts;

namespace Thunders.TechTest.OutOfBox.Repositories;

public class TopGrossingTollPlazasReportRepository : ITopGrossingTollPlazasReportRepository
{
    private readonly DefaultContext _context;
    private readonly IReportRespository _reportRespository;

    public TopGrossingTollPlazasReportRepository(DefaultContext context, IReportRespository reportRespository)
    {
        _context = context;
        _reportRespository = reportRespository;
    }

    public async Task<Report> CreateReportTransactionAsync(Report report, ICollection<TopGrossingTollPlazasReport> topGrossingTollPlazasReports, CancellationToken cancellationToken = default)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _reportRespository.CreateReport(report, cancellationToken);

            await _context.AddRangeAsync(topGrossingTollPlazasReports);
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

    public async Task<ICollection<TopGrossingTollPlazasReport>> GetTopGrossingTollPlazasReport(Guid reportId, int maxToll, CancellationToken cancellationToken = default)
    {
        var result = await _context.TollTransactions
    .Select(x => new
    {
        Year = x.CreatedAt.Year,
        Month = x.CreatedAt.Month,
        x.TollId,
        TollName = x.Toll.Name,
        x.AmountPaid
    })
    .GroupBy(x => new
    {
        x.Year,
        x.Month,
        x.TollId,
        x.TollName
    })
    .Select(g => new TopGrossingTollPlazasReport()
    {
        Id=Guid.NewGuid(),
        ReportId = reportId,
        Year = g.Key.Year,
        Month = g.Key.Month,
        TollId = g.Key.TollId,
        TollName = g.Key.TollName,
        TollTotal = g.Sum(x => x.AmountPaid),
        CreatedAt = DateTimeOffset.UtcNow
    }).OrderByDescending(x => x.TollTotal)
        .Take(maxToll)
    .ToListAsync();

        return result;
    }
}
