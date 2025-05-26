
using Thunders.TechTest.Abstractions.Messages;

namespace Thunders.TechTest.Worker.Actions.Report.CreateReport
{
    public record CreateReportRequest(Guid RequestId, ReportType ReportType, Dictionary<string, string>? Parameters, DateTimeOffset RequestedAt);

}
