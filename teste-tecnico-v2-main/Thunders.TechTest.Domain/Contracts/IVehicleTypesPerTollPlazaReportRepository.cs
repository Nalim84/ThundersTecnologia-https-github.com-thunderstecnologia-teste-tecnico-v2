using Thunders.TechTest.Domain.Entities;

namespace Thunders.TechTest.Domain.Contracts;

public interface IVehicleTypesPerTollPlazaReportRepository
{
    Task<Report> CreateReportTransactionAsync(Report report, ICollection<VehicleTypesPerTollPlazaReport> TotalPerHourPerCityReports, CancellationToken cancellationToken = default);
    Task<ICollection<VehicleTypesPerTollPlazaReport>> GetVehicleTypeCountReport(Guid reportId, Guid tollId, CancellationToken cancellationToken = default);
}
