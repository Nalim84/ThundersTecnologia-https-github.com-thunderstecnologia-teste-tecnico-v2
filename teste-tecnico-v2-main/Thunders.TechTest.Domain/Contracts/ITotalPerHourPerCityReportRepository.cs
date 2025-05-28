using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Domain.Contracts;

public interface ITotalPerHourPerCityReportRepository
{
    Task<Report> CreateReportTransaction(Report report, ICollection<TotalPerHourPerCityReport> totalPerHourPerCityReports, CancellationToken cancellationToken = default);
    Task<ICollection<TotalPerHourPerCityReport>> GetTotalPerHourPerCityReport(Guid reportId, string cityName, CancellationToken cancellationToken = default);
}
