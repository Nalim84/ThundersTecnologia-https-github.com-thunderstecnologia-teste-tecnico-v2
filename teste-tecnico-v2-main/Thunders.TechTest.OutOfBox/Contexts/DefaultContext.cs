using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Types;

namespace Thunders.TechTest.OutOfBox.Contexts;

public class DefaultContext : DbContext
{
    public DbSet<Toll> Tolls { get; set; }
    public DbSet<TollTransaction> TollTransactions { get; set; }

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

    }
}
