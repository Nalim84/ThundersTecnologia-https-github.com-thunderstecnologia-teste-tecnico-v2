using Mapster;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Types;
using DomainEntities = Thunders.TechTest.Domain.Entities;
namespace Thunders.TechTest.Application.Report.CreateReport.Strategies;

public class VehicleTypesPerTollPlazaGenerator : IReportGenerator
{
    private readonly IVehicleTypesPerTollPlazaReportRepository _repository;

    public VehicleTypesPerTollPlazaGenerator(IVehicleTypesPerTollPlazaReportRepository repository)
    {
        _repository = repository;
    }

    ReportType IReportGenerator.ReportType => ReportType.VehicleTypesPerTollPlaza;

    public async Task HandleAsync(CreateReportCommand report, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(report.Parameters["TollId"], out var tollId))
        {
            var reportDataSource = await _repository.GetVehicleTypeCountReport(report.Id, tollId, cancellationToken);
            var reportEntity = report.Adapt<DomainEntities.Report>();
            await _repository.CreateReportTransactionAsync(reportEntity, reportDataSource, cancellationToken);
        }
    }
}
