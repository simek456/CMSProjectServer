using CMSProjectServer.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CMSProjectServer.Api;

public static partial class Startup
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureServices((ctx, services) =>
        {
            services.AddLogging();
            services.AddControllers()
                .AddControllersAsServices();
        });
        return builder;
    }
}