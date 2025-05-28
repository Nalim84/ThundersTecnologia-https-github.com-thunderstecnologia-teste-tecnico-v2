using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Contexts;

namespace Thunders.TechTest.OutOfBox.Repositories;

public class VehicleTypesPerTollPlazaReportRepository : IVehicleTypesPerTollPlazaReportRepository
{
    private readonly DefaultContext _context;
    private readonly IReportRespository _reportRespository;

    public VehicleTypesPerTollPlazaReportRepository(DefaultContext context, IReportRespository reportRespository)
    {
        _context = context;
        _reportRespository = reportRespository; 
    }

    public async Task<Report> CreateReportTransactionAsync(Report report, ICollection<VehicleTypesPerTollPlazaReport> TotalPerHourPerCityReports, CancellationToken cancellationToken = default)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _reportRespository.CreateReport(report, cancellationToken);

            await _context.AddRangeAsync(TotalPerHourPerCityReports);
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

    public async Task<ICollection<VehicleTypesPerTollPlazaReport>> GetVehicleTypeCountReport(Guid reportId, Guid tollId, CancellationToken cancellationToken = default)
    {
        var query = await _context.TollTransactions
        .Where(x => x.TollId == tollId)
        .GroupBy(x => x.VehicleType)
        .Select(g => new VehicleTypesPerTollPlazaReport
        {
            Id = Guid.NewGuid(),
            ReportId = reportId,
            VehicleType = g.Key,
            Total = g.Count(),
            CreatedAt = DateTime.UtcNow
        })
        .ToListAsync();

        return query;
    }
}
