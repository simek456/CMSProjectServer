using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace CMSProjectServer;

public static partial class Startup
{
    public static WebApplication BuildApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.Lifetime.ApplicationStarted.Register(() =>
        {
            app.Logger.LogInformation(LoggingEvents.ApplicationStarted, "Application Started");
        });
        return app;
    }
}