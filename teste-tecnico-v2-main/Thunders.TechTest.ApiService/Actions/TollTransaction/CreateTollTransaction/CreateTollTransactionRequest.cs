using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.ApiService.Actions.TollTransaction.CreateTollTransaction
{
    public record CreateTollTransactionRequest(Guid Id, Guid TollId, VehicleType VehicleType, decimal AmountPaid);
}
