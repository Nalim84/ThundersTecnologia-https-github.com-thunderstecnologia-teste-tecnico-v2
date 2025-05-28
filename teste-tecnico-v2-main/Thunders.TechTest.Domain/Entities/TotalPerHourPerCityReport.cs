using Thunders.TechTest.Domain.Common;

namespace Thunders.TechTest.Domain.Entities
{
    public class TotalPerHourPerCityReport : BaseEntity
    {
        public Guid ReportId { get; set; }
        public string CityName {  get; set; }
        public int Hour { get; set; }
        public decimal TotalPerHour { get; set; }
        public Report Report { get; set; }
    }
}
