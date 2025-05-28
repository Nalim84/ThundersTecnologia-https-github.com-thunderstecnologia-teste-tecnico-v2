using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Application.Report.CreateReport.Strategies;

public interface IReportGeneratorFactory
{
    IReportGenerator GetGenerator(ReportType reportType);
}
