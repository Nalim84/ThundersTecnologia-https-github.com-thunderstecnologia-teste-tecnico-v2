using FluentValidation;

namespace Thunders.TechTest.Application.Toll.CreateToll
{
    public class CreateTollCommandValidator : AbstractValidator<CreateTollCommand>
    {
        public CreateTollCommandValidator()
        {
            RuleFor(x => x.Id)
        .NotEmpty().WithMessage("The toll identifier is required.");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("The toll name is required.")
                .MaximumLength(100).WithMessage("The toll name must be at most 100 characters long.");

            RuleFor(t => t.City)
                .NotEmpty().WithMessage("The city is required.")
                .MaximumLength(100).WithMessage("The city name must be at most 100 characters long.");

            RuleFor(t => t.State)
                .NotEmpty().WithMessage("The state is required.")
                .Length(2).WithMessage("The state must be exactly 2 characters long (abbreviation).");
        }
    }
}
