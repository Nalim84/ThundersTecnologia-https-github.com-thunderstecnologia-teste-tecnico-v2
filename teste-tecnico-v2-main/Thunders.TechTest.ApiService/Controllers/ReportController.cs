using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thunders.TechTest.Abstractions.Messages;
using Thunders.TechTest.ApiService.Actions.Report.CreateReport;
using Thunders.TechTest.ApiService.Actions.Report.CreateToll;
using Thunders.TechTest.ApiService.Common;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMessageSender _messageSender;
    private readonly IValidator<CreateReportRequest> _validator;

    public ReportController(IMessageSender messageSender, IValidator<CreateReportRequest> validator)
    {
        _messageSender = messageSender;
        _validator = validator;
    }

    /// <summary>
    /// Submits a new report creation request based on the specified report type and parameters.
    /// The request is validated and dispatched to the message bus for asynchronous processing.
    /// </summary>
    /// <param name="request">
    /// The report creation request must contain the following fields:<br/><br/>
    /// - <b>ReportType</b>: Specifies the type of report to generate. Accepted values:<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>1 - TotalPerHourPerCity</b>: Returns the total number of toll transactions per hour, grouped by city.<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>2 - TopGrossingTollPlazas</b>: Returns the toll plazas that generated the highest revenue.<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>3 - VehicleTypesPerTollPlaza</b>: Returns the count of vehicle types per toll plaza.<br/><br/>
    /// - <b>Parameters</b>: A key-value dictionary with additional parameters required by the selected report type:<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>TotalPerHourPerCity</b>: Requires key <c>"City"</c> with a non-empty string value.<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>TopGrossingTollPlazas</b>: Requires key <c>"MaxToll"</c> with a positive integer value as string.<br/>
    /// &nbsp;&nbsp;&nbsp;&nbsp;• <b>VehicleTypesPerTollPlaza</b>: Requires key <c>"TollId"</c> with a valid GUID string.<br/><br/>
    /// - <b>RequestedAt</b>: Timestamp indicating when the report was requested (DateTimeOffset).
    /// </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// Returns 202 Accepted if the request is valid and has been sent to the message bus.<br/>
    /// Returns 400 Bad Request if validation fails.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Submit a report creation request",
        Description = "Submits a new report request for asynchronous processing. The request must contain:\n\n" +
                      "- **ReportType**: Indicates the report type:\n" +
                      "  • 1 - TotalPerHourPerCity\n" +
                      "  • 2 - TopGrossingTollPlazas\n" +
                      "  • 3 - VehicleTypesPerTollPlaza\n\n" +
                      "- **Parameters**: Dictionary of required parameters based on the selected report type:\n" +
                      "  • TotalPerHourPerCity → \"City\": string\n" +
                      "  • TopGrossingTollPlazas → \"MaxToll\": integer (string format)\n" +
                      "  • VehicleTypesPerTollPlaza → \"TollId\": GUID (string format)\n\n" +
                      "- **RequestedAt**: Date and time the report was requested (ISO 8601 format)."
    )]

    [ProducesResponseType(typeof(ApiResponseWithData<CreateReportResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var message = request.Adapt<CreateReportMessage>();

        await _messageSender.SendLocal(message);

        return Accepted("Create report send to bus.");
    }
}
