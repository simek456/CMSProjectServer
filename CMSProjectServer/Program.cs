using CMSProjectServer;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServiceProvider()
    .ConfigureServices()
    .BuildApp();

app.Run();