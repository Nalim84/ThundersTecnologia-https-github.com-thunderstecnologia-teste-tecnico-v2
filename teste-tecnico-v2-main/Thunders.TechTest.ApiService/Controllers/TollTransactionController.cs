using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

    /// <summary>
    /// Submits a new toll transaction to be processed asynchronously.
    /// The request is validated and dispatched to the message bus.
    /// </summary>
    /// <param name="request">
    /// The toll transaction request must contain the following fields:<br/><br/>
    /// - <b>TollId</b>: Identifier (GUID) of the toll plaza where the transaction occurred.<br/>
    /// - <b>VehicleType</b>: Type of vehicle that passed through the toll. Must be one of the following:<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>1 - Motorcycle</b>: Two-wheeled motor vehicle.<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>2 - Car</b>: Standard passenger vehicle.<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>3 - Truck</b>: Large cargo vehicle.<br/>
    /// - <b>AmountPaid</b>: Amount paid by the driver at the toll (positive decimal value).
    /// </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// Returns 202 Accepted if the request is valid and has been sent to the message bus.<br/>
    /// Returns 400 Bad Request if validation fails.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Submit a toll transaction",
        Description = "Submits a new toll transaction for asynchronous processing. The request must include:\n\n" +
                      "- **TollId**: Identifier of the toll plaza (GUID).\n" +
                      "- **VehicleType**: Type of the vehicle. Must be one of:\n" +
                      "  • 1 - Motorcycle\n" +
                      "  • 2 - Car\n" +
                      "  • 3 - Truck\n" +
                      "- **AmountPaid**: Amount paid by the driver (decimal)."
    )]
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
