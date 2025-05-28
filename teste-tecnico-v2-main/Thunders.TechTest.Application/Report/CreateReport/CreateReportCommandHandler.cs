using FluentValidation;
using MediatR;
using Thunders.TechTest.Application.Report.CreateReport.Strategies;

namespace Thunders.TechTest.Application.Report.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportResult>
{
    private readonly IReportGeneratorFactory _reportGeneratorFactory;

    public CreateReportCommandHandler(IReportGeneratorFactory reportGeneratorFactory)
    {
        _reportGeneratorFactory = reportGeneratorFactory;
    }

    public async Task<CreateReportResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateReportCommandValidator();
                var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var generator = _reportGeneratorFactory.GetGenerator((Domain.Types.ReportType)request.ReportType);
            await generator.HandleAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            var aaaa = ex.Message;
            throw;
        }

       return null;
    }
}