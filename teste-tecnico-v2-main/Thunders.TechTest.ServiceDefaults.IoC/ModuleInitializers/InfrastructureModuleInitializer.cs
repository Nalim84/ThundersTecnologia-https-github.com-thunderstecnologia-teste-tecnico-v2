using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Thunders.TechTest.Application.Toll.CreateToll;
using Thunders.TechTest.Application.TollTransaction.TollTransaction;
using Thunders.TechTest.Domain.Contracts;
using Thunders.TechTest.OutOfBox.Repositories;

namespace Thunders.TechTest.ServiceDefaults.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRequestHandler<CreateTollTransactionCommand, CreateTollTransactionResult>, CreateTollTransactionCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<CreateTollCommand, CreateTollResult>, CreateTollHandler>();
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        builder.Services.AddScoped<ITollTransactionRepository, TollTransactionRepository>();
        builder.Services.AddScoped<ITollRepository, TollRepository>();
    }

    public void Initialize(HostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRequestHandler<CreateTollTransactionCommand, CreateTollTransactionResult>, CreateTollTransactionCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<CreateTollCommand, CreateTollResult>, CreateTollHandler>();
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        builder.Services.AddScoped<ITollTransactionRepository, TollTransactionRepository>();
        builder.Services.AddScoped<ITollRepository, TollRepository>();
    }
}
