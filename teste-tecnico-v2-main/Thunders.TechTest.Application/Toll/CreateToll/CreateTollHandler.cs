using Mapster;
using MediatR;
using Thunders.TechTest.Domain.Contracts;
using DomainEntities = Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Application.Toll.CreateToll
{
    public class CreateTollHandler : IRequestHandler<CreateTollCommand, CreateTollResult>
    {
        public readonly ITollRepository _tollRepository;

        public CreateTollHandler(ITollRepository tollRepository)
        {
            _tollRepository = tollRepository;
        }

        public async Task<CreateTollResult> Handle(CreateTollCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTollCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var existingToll = await _tollRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingToll != null)
                throw new InvalidOperationException($"Toll with id {request.Id} already exists");

            var toll = request.Adapt<DomainEntities.Toll>();
            toll.CreatedAt = DateTimeOffset.UtcNow;
            var createdTollTransaction = await _tollRepository.CreateAsync(toll, cancellationToken);
            var result = createdTollTransaction.Adapt<CreateTollResult>();

            return result;
        }
    }
}
