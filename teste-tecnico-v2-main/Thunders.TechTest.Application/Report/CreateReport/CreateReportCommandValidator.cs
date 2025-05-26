using FluentValidation;
using Thunders.TechTest.Abstractions.Messages;

namespace Thunders.TechTest.Application.Report.CreateReport;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
        RuleFor(x => x.ReportType)
            .IsInEnum()
            .WithMessage("The report type is invalid.");

        RuleFor(x => x.Parameters)
            .Must(BeValidParameters)
            .When(x => x.Parameters != null)
            .WithMessage("The report parameters must not contain empty keys or values.");
    }

    private bool BeValidParameters(Dictionary<string, string> parameters)
    {
        return parameters.All(kv => !string.IsNullOrWhiteSpace(kv.Key) && !string.IsNullOrWhiteSpace(kv.Value));
    }
}