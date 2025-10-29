using smartcity.org.apis.arch.Interfaces;
using smartcity.org.apis.device.management.Services;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Services.AddSingleton<IHealthService, HealthService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Health endpoint
app.MapGet("/health", (IHealthService health) => Results.Ok(health.Check()));

app.Run();
