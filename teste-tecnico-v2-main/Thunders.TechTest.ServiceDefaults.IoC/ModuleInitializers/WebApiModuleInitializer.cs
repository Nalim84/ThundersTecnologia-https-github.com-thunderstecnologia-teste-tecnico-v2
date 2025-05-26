using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Thunders.TechTest.ServiceDefaults.IoC.ModuleInitializers;

public class WebApiModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
      
    }

    public void Initialize(HostApplicationBuilder builder)
    {
        throw new NotImplementedException();
    }
}
