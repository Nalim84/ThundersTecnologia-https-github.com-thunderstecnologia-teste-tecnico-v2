using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Domain.Contracts;

public interface IReportRespository
{
    Task<Report> CreateReport(Report report, CancellationToken cancellationToken);
}
