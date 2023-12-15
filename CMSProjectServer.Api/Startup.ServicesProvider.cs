using Autofac;
using Autofac.Extensions.DependencyInjection;
using CMSProjectServer.Core.DI;
using Microsoft.AspNetCore.Builder;

namespace CMSProjectServer.Api;

public static partial class Startup
{
    public static WebApplicationBuilder ConfigureServiceProvider(this WebApplicationBuilder builder)
    {
        builder.Host
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((ctx, cb) => DIContainer.BuildContainer(cb, ctx.Configuration));
        return builder;
    }
}