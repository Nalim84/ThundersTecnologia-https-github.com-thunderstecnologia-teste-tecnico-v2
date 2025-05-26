using Thunders.TechTest.Domain.Common;

namespace Thunders.TechTest.Domain.Entities;

public class Toll : BaseEntity
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }

    public ICollection<TollTransaction>? TollTransactions { get; set; }
}
