using MediatR;
using Thunders.TechTest.Application.Toll.CreateToll;
using Thunders.TechTest.Domain.Contracts;

namespace Thunders.TechTest.Application.Report.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportResult>
{
    public readonly ITollTransactionRepository _tollTransactionRepository;
    public readonly ITollRepository _tollRepository;

    public CreateReportCommandHandler(ITollTransactionRepository tollTransactionRepository, ITollRepository tollRepository)
    {
        _tollTransactionRepository = tollTransactionRepository;
        _tollRepository = tollRepository;
    }

    public async Task<CreateReportResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateReportCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);


        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);



        return null;
    }
}