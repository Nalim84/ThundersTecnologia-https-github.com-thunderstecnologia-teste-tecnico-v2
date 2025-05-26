using FluentValidation;

namespace Thunders.TechTest.Application.Report.CreateReport;

public record CreateReportResult
{
    public Guid Id { get; set; }
}
