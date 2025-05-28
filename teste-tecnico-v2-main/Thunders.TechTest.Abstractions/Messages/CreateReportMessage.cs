namespace Thunders.TechTest.Abstractions.Messages;

public class CreateReportMessage
{
    public Guid Id { get; set; }
    public ReportType ReportType { get; set; }
    public Dictionary<string, string>? Parameters { get; set; }
    public DateTimeOffset RequestedAt { get; set; }

    public CreateReportMessage()
    {
    }

    public CreateReportMessage(Guid Id, ReportType reportType, Dictionary<string, string>? parameters = null)
    {
        ReportType = reportType;
        Parameters = parameters;
        RequestedAt = DateTimeOffset.UtcNow;
    }
}

public enum ReportType
{
    TotalPerHourPerCity = 1,
    TopGrossingTollPlazas = 2,
    VehicleTypesPerTollPlaza = 3
}