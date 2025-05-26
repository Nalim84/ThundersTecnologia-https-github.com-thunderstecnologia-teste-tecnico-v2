using FluentValidation;
using Thunders.TechTest.Abstractions.Messages;

namespace Thunders.TechTest.Application.TollTransaction.TollTransaction;

public class CreateTollTransactionCommandValidator : AbstractValidator<CreateTollTransactionCommand>
{
    public CreateTollTransactionCommandValidator()
    {
        RuleFor(x => x.TollId)
            .NotEmpty().WithMessage("O identificador do pedágio (TollId) é obrigatório.");

        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Tipo de veículo inválido.");

        RuleFor(x => x.AmountPaid)
            .GreaterThan(0).WithMessage("O valor pago deve ser maior que zero.");
    }
}
