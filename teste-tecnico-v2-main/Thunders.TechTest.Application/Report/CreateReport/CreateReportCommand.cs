using MediatR;
using Thunders.TechTest.Abstractions.Messages;

namespace Thunders.TechTest.Application.Report.CreateReport
{
    public record CreateReportCommand(Guid Id, ReportType ReportType, Dictionary<string, string>? Parameters, DateTimeOffset RequestedAt) : IRequest<CreateReportResult>
    { }
}
