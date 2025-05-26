using Mapster;
using MediatR;
using Thunders.TechTest.Domain.Contracts;
using DomainEntities = Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Application.TollTransaction.TollTransaction;

public class CreateTollTransactionCommandHandler : IRequestHandler<CreateTollTransactionCommand, CreateTollTransactionResult>
{
    public readonly ITollTransactionRepository _tollTransactionRepository;
    public readonly ITollRepository _tollRepository;

    public CreateTollTransactionCommandHandler(ITollTransactionRepository tollTransactionRepository, ITollRepository tollRepository)
    {
        _tollTransactionRepository = tollTransactionRepository;
        _tollRepository = tollRepository;
    }

    public async Task<CreateTollTransactionResult> Handle(CreateTollTransactionCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTollTransactionCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);

        var existingToll = await _tollRepository.GetByIdAsync(request.TollId, cancellationToken);
        
        if (existingToll == null)
            throw new InvalidOperationException($"Toll with id {request.TollId} not exists");

        var tollTransaction = request.Adapt<DomainEntities.TollTransaction>();
        tollTransaction.CreatedAt = DateTimeOffset.UtcNow;

        var createdTollTransaction = await _tollTransactionRepository.CreateAsync(tollTransaction, cancellationToken);
        var result = createdTollTransaction.Adapt<CreateTollTransactionResult>();

        return result;
    }
}
