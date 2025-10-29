using smartcity.org.apis.arch.Interfaces;
using smartcity.org.apis.energy.monitor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHealthService, HealthService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/health", (IHealthService health) => Results.Ok(health.Check()));

app.Run();
