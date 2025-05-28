using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Application.Report.CreateReport.Strategies;

public class ReportGeneratorFactory : IReportGeneratorFactory
{
    private readonly IEnumerable<IReportGenerator> _generators;

    public ReportGeneratorFactory(IEnumerable<IReportGenerator> generators)
    {
        _generators = generators;
    }

    public IReportGenerator GetGenerator(ReportType reportType)
    {
        var generator = _generators.FirstOrDefault(g => g.ReportType == reportType);
        
        if (generator == null)
            throw new InvalidOperationException($"No generator found for report type {reportType}");

        return generator;
    }
}