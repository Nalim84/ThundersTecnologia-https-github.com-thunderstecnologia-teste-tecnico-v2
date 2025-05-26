using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.Migration;
using Thunders.TechTest.OutOfBox.Contexts;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<DefaultContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("ThundersTechTestDb"))
);

builder.Services.AddOpenTelemetry()
        .WithTracing(tracing => tracing.AddSource(Worker.ActivityName));

var host = builder.Build();
host.Run();