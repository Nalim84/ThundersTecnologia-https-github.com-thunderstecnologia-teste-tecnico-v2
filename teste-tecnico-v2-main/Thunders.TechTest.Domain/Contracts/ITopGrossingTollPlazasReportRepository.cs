using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Domain.Contracts;

public interface ITopGrossingTollPlazasReportRepository
{
    Task<Report> CreateReportTransactionAsync(Report report, ICollection<TopGrossingTollPlazasReport> topGrossingTollPlazasReports, CancellationToken cancellationToken = default);
    Task<ICollection<TopGrossingTollPlazasReport>> GetTopGrossingTollPlazasReport(Guid reportId, int maxToll, CancellationToken cancellationToken = default);
}
