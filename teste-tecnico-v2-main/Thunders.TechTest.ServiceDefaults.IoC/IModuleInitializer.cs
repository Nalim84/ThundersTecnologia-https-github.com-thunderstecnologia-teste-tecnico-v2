using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Thunders.TechTest.ServiceDefaults.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
    void Initialize(HostApplicationBuilder builder);
}
