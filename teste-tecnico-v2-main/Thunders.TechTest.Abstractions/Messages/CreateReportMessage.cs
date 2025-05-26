namespace Thunders.TechTest.Abstractions.Messages;

public class CreateReportMessage
{
    public Guid RequestId { get; set; } = Guid.NewGuid();

    public ReportType ReportType { get; set; }
   
    public Dictionary<string, string>? Parameters { get; set; }

    public DateTimeOffset RequestedAt { get; set; } = DateTime.UtcNow;
}

public enum ReportType
{
    TotalPorHoraPorCidade = 1,
    PracasMaisFaturaram = 2,
    VeiculosPorPraca = 3
}