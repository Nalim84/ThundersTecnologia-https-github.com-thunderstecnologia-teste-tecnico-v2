using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.OutOfBox.Contexts;

public class DefaultContext : DbContext
{
    public DbSet<Toll> Tolls { get; set; }
    public DbSet<TollTransaction> TollTransactions { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<TopGrossingTollPlazasReport> TopGrossingTollPlazasReports { get; set; }
    public DbSet<TotalPerHourPerCityReport> TotalPerHourPerCityReports { get; set; }
    public DbSet<VehicleTypesPerTollPlazaReport> VehicleTypesPerTollPlazaReports { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var tollTransactionBuilder =
            builder.Entity<TollTransaction>();

        tollTransactionBuilder
                       .Property(e => e.VehicleType)
                       .IsRequired()
                       .HasMaxLength(100)
                       .HasConversion(new EnumToStringConverter<VehicleType>());

        tollTransactionBuilder
            .Property(t => t.AmountPaid)
        .HasPrecision(10, 2);

        var topGrossingTollPlazasReport = builder.Entity< TopGrossingTollPlazasReport>();

        topGrossingTollPlazasReport
            .Property(x => x.TollTotal)
        .HasPrecision(10, 2);

        var totalPerHourPerCityReport = builder.Entity<TotalPerHourPerCityReport>();
        totalPerHourPerCityReport
            .Property(x => x.TotalPerHour)
            .HasPrecision(10, 2);

    }
}
