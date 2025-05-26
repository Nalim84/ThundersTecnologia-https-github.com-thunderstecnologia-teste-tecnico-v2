using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Worker.Actions.TollTransaction.CreateTollTransaction
{
    public record CreateTollTransactionRequest(Guid TollId, VehicleType VehicleType, decimal AmountPaid);
}
