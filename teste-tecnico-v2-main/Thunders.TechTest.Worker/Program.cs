using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;
using Thunders.TechTest.Abstractions.Messages;
using Thunders.TechTest.IoC;
using Thunders.TechTest.OutOfBox.Contexts;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.Worker;
using Thunders.TechTest.Worker.Handlers.CreateTollTransaction;

using System.Text.Json;
using Thunders.TechTest.Worker.Handlers.CreateReport;
using Mapster;
using Thunders.TechTest.Application.Report.CreateReport;
using Thunders.TechTest.Domain.Entities;


var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AutoRegisterHandlersFromAssemblyOf<CreateTollTransactionHandler>();
builder.Services.AutoRegisterHandlersFromAssemblyOf<CreateReportHandler>();

builder.Services.AddRebus(configure =>
    configure
        .Transport(t => t.UseRabbitMq(builder.Configuration["ConnectionStrings:RabbitMq"], "Thunders.TechTest"))
        .Serialization(s => s.UseNewtonsoftJson())
        .Routing(r => r.TypeBased().MapAssemblyOf<CreateTollTransactionMessage>("Thunders.TechTest"))
        .Options(o =>
        {
            o.SetMaxParallelism(5); // número de mensagens processadas ao mesmo tempo
            o.SetNumberOfWorkers(2); // número de threads de consumo
           
        })
);

TypeAdapterConfig<CreateReportCommand, Report>
    .NewConfig()
    .AfterMapping((src, dest) =>
    {
        dest.Parameters = src.Parameters == null ? null : JsonSerializer.Serialize(src.Parameters);
    });


DependencyResolver.RegisterHostApplicationDependencies(builder);

builder.Services.AddSqlServerDbContext<DefaultContext>(builder.Configuration);
builder.Services.AddHostedService<Worker>();


var host = builder.Build();
await host.RunAsync();