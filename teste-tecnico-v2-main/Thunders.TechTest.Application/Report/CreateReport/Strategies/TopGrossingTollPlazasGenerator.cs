using Mapster;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Types;
using DomainEntities = Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Application.Report.CreateReport.Strategies;

public class TopGrossingTollPlazasGenerator : IReportGenerator
{
    private readonly ITopGrossingTollPlazasReportRepository _repository;

    public TopGrossingTollPlazasGenerator(ITopGrossingTollPlazasReportRepository repository)
    {
        _repository = repository;
    }

    ReportType IReportGenerator.ReportType => ReportType.TopGrossingTollPlazas;

    public async Task HandleAsync(CreateReportCommand report, CancellationToken cancellationToken)
    {
        if (int.TryParse(report.Parameters["MaxToll"], out var maxToll))
        {
            var reportDataSource = await _repository.GetTopGrossingTollPlazasReport(report.Id, maxToll, cancellationToken);
            var reportEntity = report.Adapt<DomainEntities.Report>();
            var response = await _repository.CreateReportTransactionAsync(reportEntity, reportDataSource ,cancellationToken);
        }
    }
}