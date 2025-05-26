using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.ApiService.Actions.Toll.CreateToll
{
    public record CreateTollRequest(Guid Id, string Name, string City, string State);
}
