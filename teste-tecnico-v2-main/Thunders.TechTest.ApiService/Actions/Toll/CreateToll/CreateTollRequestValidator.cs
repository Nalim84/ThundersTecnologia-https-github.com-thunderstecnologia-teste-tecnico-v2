using FluentValidation;

namespace Thunders.TechTest.ApiService.Actions.Toll.CreateToll
{
    public class CreateTollRequestValidator : AbstractValidator<CreateTollRequest>
    {
        public CreateTollRequestValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("The toll identifier is required.");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("The toll name is required.")
                .MaximumLength(100).WithMessage("The toll name must be at most 100 characters long.");

            RuleFor(t => t.City)
                .NotEmpty().WithMessage("The city is required.")
                .MaximumLength(100).WithMessage("The city must be at most 100 characters long.");

            RuleFor(t => t.State)
                .NotEmpty().WithMessage("The state is required.")
                .Length(2).WithMessage("The state must contain exactly 2 characters (abbreviation).");

        }
    }
}
