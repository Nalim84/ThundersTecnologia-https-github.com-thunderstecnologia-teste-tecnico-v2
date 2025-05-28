using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thunders.TechTest.ApiService.Actions.Toll.CreateToll;
using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.Application.Toll.CreateToll;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TollController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMessageSender _messageSender;
    private readonly IValidator<CreateTollRequest> _validator;

    public TollController(IMediator mediator, IMessageSender messageSender, IValidator<CreateTollRequest> validator)
    {
        _mediator = mediator;
        _messageSender = messageSender;
        _validator = validator;
    }

    /// <summary>
    /// Creates a new toll plaza with the specified details.
    /// The request is validated and processed using the mediator pattern to persist the toll data.
    /// </summary>
    /// <param name="request">
    /// The toll creation request containing the following fields:<br/>
    /// - <b>Id</b>: Unique identifier for the toll (non-empty GUID).<br/>
    /// - <b>Name</b>: Name of the toll plaza (non-empty string).<br/>
    /// - <b>City</b>: City where the toll is located (non-empty string).<br/>
    /// - <b>State</b>: State where the toll is located (non-empty string).
    /// </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// Returns 201 Created with the created toll details if the request is valid.<br/>
    /// Returns 400 Bad Request if validation fails.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new toll plaza",
        Description = "Creates a new toll plaza with specified information:\n\n" +
                      "- **Id**: Unique identifier for the toll (GUID).\n" +
                      "- **Name**: Name of the toll plaza.\n" +
                      "- **City**: City where the toll is located.\n" +
                      "- **State**: State where the toll is located."
    )]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateTollResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateToll([FromBody] CreateTollRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = request.Adapt<CreateTollCommand>();
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateTollResponse>
            {
                Success = true,
                Message = "Toll Transaction created successfully",
                Data = response.Adapt<CreateTollResponse>()
            });
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
