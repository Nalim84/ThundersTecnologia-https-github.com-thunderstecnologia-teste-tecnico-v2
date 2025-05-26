using FluentValidation;
using Thunders.TechTest.Abstractions.Messages;

namespace Thunders.TechTest.Worker.Actions.TollTransaction.CreateTollTransaction;

public class CreateTollTransactionMessageValidator : AbstractValidator<CreateTollTransactionMessage>
{
    public CreateTollTransactionMessageValidator()
    {
        RuleFor(x => x.TollId)
            .NotEmpty().WithMessage("The toll identifier (TollId) is required.");

        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Invalid vehicle type.");

        RuleFor(x => x.AmountPaid)
            .GreaterThan(0).WithMessage("The amount paid must be greater than zero.");
    }
}
