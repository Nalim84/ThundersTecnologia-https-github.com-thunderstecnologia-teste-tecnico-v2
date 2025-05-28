using FluentValidation;

namespace Thunders.TechTest.Application.TollTransaction.TollTransaction;

public class CreateTollTransactionCommandValidator : AbstractValidator<CreateTollTransactionCommand>
{
    public CreateTollTransactionCommandValidator()
    {
        RuleFor(x => x.TollId)
      .NotEmpty().WithMessage("The toll identifier (TollId) is required.");

        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Invalid vehicle type.");

        RuleFor(x => x.AmountPaid)
            .GreaterThan(0).WithMessage("The amount paid must be greater than zero.");
    }
}
