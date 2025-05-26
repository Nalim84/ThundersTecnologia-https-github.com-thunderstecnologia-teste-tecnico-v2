using Thunders.TechTest.Domain.Common;
using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Domain.Entities;

public class TollTransaction : BaseEntity
{
    public Guid TollId { get; set; }
    public VehicleType VehicleType { get; set; }
    public decimal AmountPaid { get; set; }
    public Toll Toll { get; set; }
}
