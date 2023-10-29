using Autofac;
using CMSProjectServer.DI;
using Microsoft.AspNetCore.Builder;

namespace CMSProjectServer;

public static partial class Startup
{
    public static WebApplicationBuilder ConfigureServiceProvider(this WebApplicationBuilder builder)
    {
        builder.Host
            .ConfigureContainer<ContainerBuilder>((ctx, cb) => DIContainer.BuildContainer(cb, ctx.Configuration));
        return builder;
    }
}