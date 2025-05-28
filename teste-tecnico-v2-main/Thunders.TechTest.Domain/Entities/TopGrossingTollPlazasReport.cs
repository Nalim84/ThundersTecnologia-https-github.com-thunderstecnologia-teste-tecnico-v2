
using Thunders.TechTest.Domain.Common;

namespace Thunders.TechTest.Domain.Entities;

public class TopGrossingTollPlazasReport : BaseEntity
{
    public Guid ReportId { get; set; }
    public int Year { get; set ; }
    public int Month { get; set; }
    public Guid TollId {  get; set; }
    public string TollName {  get; set; }
    public decimal TollTotal {  get; set; }
    public Report Report { get; set; }
}
