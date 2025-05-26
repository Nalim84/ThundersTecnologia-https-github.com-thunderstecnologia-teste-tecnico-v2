using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using System.Diagnostics;
using Thunders.TechTest.OutOfBox.Contexts;

namespace Thunders.TechTest.Migration;

public class Worker : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;

    internal const string ActivityName = "Migration";
    private static readonly ActivitySource _activitySource = new(ActivityName);

    public Worker(IServiceProvider serviceProvider,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger)
    {
        this.serviceProvider = serviceProvider;
        this.hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Thunders.TechTest - Migration: Starting database migration process...");

        using var activity = _activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            _logger.LogInformation("Thunders.TechTest - Migration: Service scope successfully created.");

            var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            _logger.LogInformation("Thunders.TechTest - Migration: DbContext resolved: {DbContext}", dbContext.GetType().Name);

            _logger.LogInformation("Thunders.TechTest - Migration: Running database migrations...");
            await dbContext.Database.MigrateAsync(stoppingToken);
            _logger.LogInformation("Thunders.TechTest - Migration: Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            _logger.LogError(ex, "Thunders.TechTest - Migration: An error occurred while applying database migrations.");
            throw;
        }
        finally
        {
            _logger.LogInformation("Thunders.TechTest - Migration: Shutting down application.");
            hostApplicationLifetime.StopApplication();
        }
    }
}
