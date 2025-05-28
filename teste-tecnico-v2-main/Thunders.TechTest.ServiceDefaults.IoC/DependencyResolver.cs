using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Thunders.TechTest.ServiceDefaults.IoC.ModuleInitializers;

namespace Thunders.TechTest.IoC;

public static class DependencyResolver
{
    public static void RegisterWebApplicationDependencies(this WebApplicationBuilder builder)
    {
        new InfrastructureModuleInitializer().Initialize(builder);
    }
    public static void RegisterHostApplicationDependencies(this HostApplicationBuilder builder)
    {
        new InfrastructureModuleInitializer().Initialize(builder);
    }
}
