using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.ApiService.Actions.Report.CreateReport;

public record CreateReportRequest(Guid Id, ReportType ReportType ,Dictionary<string, string>? Parameters
    , DateTimeOffset RequestedAt);

