using Thunders.TechTest.Domain.Types;
using MediatR;

namespace Thunders.TechTest.Application.TollTransaction.TollTransaction;

public record CreateTollTransactionCommand(Guid Id, Guid TollId, VehicleType VehicleType, decimal AmountPaid) : IRequest<CreateTollTransactionResult>
{

}
