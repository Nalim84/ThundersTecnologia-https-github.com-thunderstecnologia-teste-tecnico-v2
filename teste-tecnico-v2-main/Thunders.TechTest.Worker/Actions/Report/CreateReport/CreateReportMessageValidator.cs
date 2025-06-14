﻿using FluentValidation;
using Thunders.TechTest.Abstractions.Messages;

namespace Thunders.TechTest.Worker.Actions.Report.CreateReport;

public class CreateReportMessageValidator : AbstractValidator<CreateReportMessage>
{
    public CreateReportMessageValidator()
    {
        RuleFor(x => x.ReportType)
            .IsInEnum()
            .WithMessage("The report type is invalid.");

        //Valor total por hora por cidade
        When(x => x.ReportType == ReportType.TotalPerHourPerCity, () =>
        {
            RuleFor(x => x.Parameters)
                .NotNull().WithMessage("Parameters must not be null.")
                .Must(p => p.Count > 0).WithMessage("Parameters must contain at least one item.")
                .Must(p => p.ContainsKey("City") && !string.IsNullOrWhiteSpace(p["City"]))
                .WithMessage("If provided, City must not be empty.");
        });

        //As praças que mais faturaram por mês (a quantidade a ser processada deve ser configurável)
        When(x => x.ReportType == ReportType.TopGrossingTollPlazas, () =>
        {
            RuleFor(x => x.Parameters)
                .NotNull()
                .WithMessage("Parameters are required for this report type.")
                .Must(p => p.Count > 0)
                .WithMessage("Parameters must contain at least one item.")
                .Must(p =>
                    p.ContainsKey("MaxToll") &&
                    !string.IsNullOrWhiteSpace(p["MaxToll"]) &&
                    int.TryParse(p["MaxToll"], out var val) &&
                    val > 0)
                .WithMessage("MaxToll is required and must be a positive integer.");
        });


        //Quantos tipos de veículos passaram em uma determinada praça
        When(x => x.ReportType == ReportType.VehicleTypesPerTollPlaza, () =>
        {
            RuleFor(x => x.Parameters)
           .NotNull()
           .WithMessage("Parameters are required for this report type.")
           .Must(p => p.Count > 0)
           .WithMessage("Parameters must contain at least one item.")
           .Must(p =>
               p.ContainsKey("TollId") &&
               !string.IsNullOrWhiteSpace(p["TollId"]) &&
               Guid.TryParse(p["TollId"], out var guid) &&
               guid != Guid.Empty)
           .WithMessage("TollId parameter is required, must be a valid GUID, and cannot be empty.");
        });
    }
}