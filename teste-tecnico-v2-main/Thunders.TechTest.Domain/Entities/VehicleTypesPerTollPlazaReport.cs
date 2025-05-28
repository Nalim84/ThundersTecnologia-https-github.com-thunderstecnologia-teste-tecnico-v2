using Thunders.TechTest.Domain.Common;
using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Domain.Entities;

public class VehicleTypesPerTollPlazaReport : BaseEntity
{
    public Guid ReportId { get; set; }
    public VehicleType VehicleType { get; set; }
    public int Total { get; set; }

    public Report Report { get; set; }
}
