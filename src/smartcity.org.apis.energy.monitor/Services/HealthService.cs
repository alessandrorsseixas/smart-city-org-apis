using smartcity.org.apis.arch.Interfaces;
using smartcity.org.apis.arch.Models;

namespace smartcity.org.apis.energy.monitor.Services;

public class HealthService : IHealthService
{
    private readonly DateTimeOffset _start = DateTimeOffset.UtcNow;

    public HealthResponse Check()
    {
        var uptime = DateTimeOffset.UtcNow - _start;
        return new HealthResponse("Healthy", DateTimeOffset.UtcNow, $"Uptime: {uptime:c}");
    }
}
