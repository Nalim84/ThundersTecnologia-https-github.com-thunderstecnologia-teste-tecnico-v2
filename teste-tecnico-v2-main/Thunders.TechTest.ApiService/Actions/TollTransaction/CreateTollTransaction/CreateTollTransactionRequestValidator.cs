using FluentValidation;

namespace Thunders.TechTest.ApiService.Actions.TollTransaction.CreateTollTransaction;

public class CreateTollTransactionRequestValidator : AbstractValidator<CreateTollTransactionRequest>
{
    public CreateTollTransactionRequestValidator()
    {
        RuleFor(x => x.TollId)
     .NotEmpty().WithMessage("The toll identifier (TollId) is required.");

        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Invalid vehicle type.");

        RuleFor(x => x.AmountPaid)
            .GreaterThan(0).WithMessage("The amount paid must be greater than zero.");
    }
}
