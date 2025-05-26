using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.Abstractions.Messages;
using Thunders.TechTest.ApiService.Actions.TollTransaction.CreateTollTransaction;
using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TollTransactionController : ControllerBase
{
    private readonly IMessageSender _messageSender;

    public TollTransactionController(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateTollTransactionResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTollTransaction([FromBody] CreateTollTransactionRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateTollTransactionRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
      
        var message = request.Adapt<CreateTollTransactionMessage>();
        message.Id = Guid.NewGuid();
        await _messageSender.SendLocal(message);

        return Accepted("Toll transaction send to bus.");
    }
}
