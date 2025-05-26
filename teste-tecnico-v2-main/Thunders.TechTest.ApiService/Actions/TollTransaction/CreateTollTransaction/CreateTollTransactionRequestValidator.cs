using FluentValidation;

namespace Thunders.TechTest.ApiService.Actions.TollTransaction.CreateTollTransaction;

public class CreateTollTransactionRequestValidator : AbstractValidator<CreateTollTransactionRequest>
{
    public CreateTollTransactionRequestValidator()
    {
        RuleFor(x => x.TollId)
            .NotEmpty().WithMessage("O identificador do pedágio (TollId) é obrigatório.");

        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Tipo de veículo inválido.");

        RuleFor(x => x.AmountPaid)
            .GreaterThan(0).WithMessage("O valor pago deve ser maior que zero.");
    }
}
