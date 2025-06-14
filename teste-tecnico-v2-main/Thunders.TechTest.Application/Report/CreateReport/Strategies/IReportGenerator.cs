﻿
using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Application.Report.CreateReport.Strategies
{
    public interface IReportGenerator
    {
        ReportType ReportType { get; }
        Task<CreateReportResult> HandleAsync(CreateReportCommand report, CancellationToken cancellationToken);
    }
}
