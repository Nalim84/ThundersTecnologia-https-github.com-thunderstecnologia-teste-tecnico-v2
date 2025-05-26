using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Worker.Actions.TollTransaction.CreateTollTransaction;

public record CreateTollTransactionResponse
{
    public Guid Id { get; set; }
    public Guid TollId { get; set; }
    public VehicleType VehicleType { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
