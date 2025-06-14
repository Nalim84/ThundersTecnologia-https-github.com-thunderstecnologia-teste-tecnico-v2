﻿using Mapster;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.Domain.Types;
using DomainEntities = Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Application.Report.CreateReport.Strategies;

public class TotalPerHourPerCityGenerator : IReportGenerator
{
    private readonly ITotalPerHourPerCityReportRepository _repository;

    public TotalPerHourPerCityGenerator(ITotalPerHourPerCityReportRepository repository)
    {
        _repository = repository;
    }

    ReportType IReportGenerator.ReportType => ReportType.TotalPerHourPerCity;

    public async Task<CreateReportResult> HandleAsync(CreateReportCommand report, CancellationToken cancellationToken)
    {
        var result = new CreateReportResult();

        if (!string.IsNullOrEmpty(report.Parameters["City"]))
        {
            var reportDataSource = await _repository.GetTotalPerHourPerCityReport(report.Id, report.Parameters["City"] ,cancellationToken);
            var reportEntity = report.Adapt<DomainEntities.Report>();
            var response = await _repository.CreateReportTransaction(reportEntity, reportDataSource, cancellationToken);
            result.Id = report.Id;
        }

        return result;
    }
}