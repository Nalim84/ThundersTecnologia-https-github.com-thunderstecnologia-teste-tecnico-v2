using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Thunders.TechTest.Abstractions.Messages;
using Thunders.TechTest.ApiService.Actions.Report.CreateReport;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.OutOfBox.Queues;
using Xunit;
using Domain = Thunders.TechTest.Domain;

namespace Thunders.TechTest.Tests.Controllers;

public class ReportControllerTests
{
    private readonly Mock<IMessageSender> _messageSenderMock;
    private readonly Mock<IValidator<CreateReportRequest>> _validatorMock;
    private readonly ReportController _controller;

    public ReportControllerTests()
    {
        _messageSenderMock = new Mock<IMessageSender>();
        _validatorMock = new Mock<IValidator<CreateReportRequest>>();
        _controller = new ReportController(_messageSenderMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task CreateReport_ShouldReturnBadRequest_WhenValidationFails()
    {
        // Arrange
        var request = new CreateReportRequest(Guid.NewGuid(), Domain .Types. ReportType.TotalPerHourPerCity, null, DateTimeOffset.UtcNow);
        var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new("Parameters", "Parameters must not be null.")
            });

        _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(validationResult);

        // Act
        var result = await _controller.CreateReport(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequest = result as BadRequestObjectResult;
        badRequest!.Value.Should().BeEquivalentTo(validationResult.Errors);

        _messageSenderMock.Verify(m => m.SendLocal(It.IsAny<object>()), Times.Never);
    }

    [Fact]
    public async Task CreateReport_ShouldReturnAccepted_WhenValidationPasses()
    {
        // Arrange
        var request = new CreateReportRequest(
            Guid.NewGuid(),
           Domain.Types.ReportType.TotalPerHourPerCity,
            new Dictionary<string, string> { { "City", "São Paulo" } },
            DateTimeOffset.UtcNow);

        var validationResult = new ValidationResult(); 

        _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(validationResult);

        _messageSenderMock.Setup(m => m.SendLocal(It.IsAny<object>()))
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateReport(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<AcceptedResult>();
        var accepted = result as AcceptedResult;

        _messageSenderMock.Verify(m => m.SendLocal(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task CreateReport_ShouldSendCorrectMessageToBus()
    {
        // Arrange
        var request = new CreateReportRequest(
            Guid.NewGuid(),
            Domain.Types.ReportType.TopGrossingTollPlazas,
            new Dictionary<string, string> { { "MaxToll", "10" } },
            DateTimeOffset.UtcNow);

        _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        CreateReportMessage? sentMessage = null;

        _messageSenderMock.Setup(m => m.SendLocal(It.IsAny<object>()))
            .Callback<object>(msg => sentMessage = msg as CreateReportMessage)
            .Returns(Task.CompletedTask);

        // Act
        await _controller.CreateReport(request, CancellationToken.None);

        // Assert
        sentMessage.Should().NotBeNull();
        sentMessage!.Id.Should().Be(request.Id);
        sentMessage.Parameters.Should().BeEquivalentTo(request.Parameters);
    }
}