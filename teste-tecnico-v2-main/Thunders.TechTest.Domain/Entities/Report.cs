using Thunders.TechTest.Domain.Common;
using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.Domain.Entities;

public class Report : BaseEntity 
{
    public ReportType ReportType { get; set; }
    public string? Parameters { get; set; } 
    public DateTimeOffset RequestedAt { get; set; }
}
