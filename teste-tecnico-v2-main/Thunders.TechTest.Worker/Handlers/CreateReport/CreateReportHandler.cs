using Mapster;
using MediatR;
using Rebus.Bus;
using Rebus.Handlers;
using Thunders.TechTest.Abstractions.Messages;
using Thunders.TechTest.Application.Toll.CreateToll;
using Thunders.TechTest.Application.TollTransaction.TollTransaction;
using Thunders.TechTest.Worker.Actions.Report.CreateReport;

namespace Thunders.TechTest.Worker.Handlers.CreateReport;

public class CreateReportHandler : IHandleMessages<CreateReportMessage>
{
    public readonly IMediator _mediator;
    private readonly IBus _bus;
    private readonly ILogger<CreateReportHandler> _logger;

    public CreateReportHandler(IMediator mediator, IBus bus, ILogger<CreateReportHandler> logger)
    {
        _mediator = mediator;
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(CreateReportMessage message)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        try
        {

            _logger.LogInformation($"CreateReportMessage - processando {message.RequestId}");
            var validator = new CreateReportMessageValidator();
            var validationResult = validator.Validate(message);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Validação falhou para {TollId}: {Errors}", message.RequestId, errors);
                return;
            }

            var command = message.Adapt<CreateTollCommand>();
            await _mediator.Send(command, cts.Token);

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, $"Erro ao processar mensagem {message.RequestId}");
            throw;
        }

    }
}
