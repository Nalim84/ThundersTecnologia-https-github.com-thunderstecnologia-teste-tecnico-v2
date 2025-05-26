using MediatR;

namespace Thunders.TechTest.Application.Toll.CreateToll;

public record CreateTollCommand(Guid Id, string Name, string City, string State) : IRequest<CreateTollResult>
{

}
