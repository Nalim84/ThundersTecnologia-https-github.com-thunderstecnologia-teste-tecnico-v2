using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.OutOfBox.Contexts;

namespace Thunders.TechTest.OutOfBox.Repositories;

public class ReportRepository : IReportRespository
{
    private readonly DefaultContext _context;

    public ReportRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Report> CreateReport(Report report, CancellationToken cancellationToken)
    {
        await _context.Reports.AddAsync(report);
        await _context.SaveChangesAsync();
        return report;
    }
}
