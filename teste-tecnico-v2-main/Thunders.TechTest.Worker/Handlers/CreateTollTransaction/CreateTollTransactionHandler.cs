using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using Thunders.TechTest.Abstractions.Messages;
using Thunders.TechTest.Application.TollTransaction.TollTransaction;
using Thunders.TechTest.Worker.Actions.TollTransaction.CreateTollTransaction;


namespace Thunders.TechTest.Worker.Handlers.CreateTollTransaction;

public class CreateTollTransactionHandler : IHandleMessages<CreateTollTransactionMessage>
{
    public readonly IMediator _mediator;
    private readonly IBus _bus;
    private readonly ILogger<CreateTollTransactionHandler> _logger;

    public CreateTollTransactionHandler(IMediator mediator, IBus bus, ILogger<CreateTollTransactionHandler> logger)
    {
        _mediator = mediator;
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(CreateTollTransactionMessage message)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        try
        {
            _logger.LogInformation($"CreateTollTransactionHandler - processing {message.TollId}");

            var validator = new CreateTollTransactionMessageValidator();
            var validationResult = validator.Validate(message);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Validation failed for {TollId}: {Errors}", message.TollId, errors);
                throw new ValidationException(errors);
            }

            var command = message.Adapt<CreateTollTransactionCommand>();
            await _mediator.Send(command, cts.Token);
        }
        catch (InvalidOperationException opex)
        {
            _logger.LogWarning(opex, $"CreateTollTransactionHandler - Business rule violation {message.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"CreateTollTransactionHandler -  Unhandled error {message.Id}");
            throw;
        }
    }
}