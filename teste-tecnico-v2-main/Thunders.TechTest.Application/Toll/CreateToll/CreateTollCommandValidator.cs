using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.Application.TollTransaction.TollTransaction;

namespace Thunders.TechTest.Application.Toll.CreateToll
{
    public class CreateTollCommandValidator : AbstractValidator<CreateTollCommand>
    {
        public CreateTollCommandValidator()
        {
            RuleFor(x => x.Id)
    .NotEmpty().WithMessage("O identificador do pedágio é obrigatório.");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("O nome do pedágio é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do pedágio deve ter no máximo 100 caracteres.");

            RuleFor(t => t.City)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(100).WithMessage("A cidade deve ter no máximo 100 caracteres.");

            RuleFor(t => t.State)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .Length(2).WithMessage("O estado deve conter exatamente 2 caracteres (sigla).");
        }
    }
}
