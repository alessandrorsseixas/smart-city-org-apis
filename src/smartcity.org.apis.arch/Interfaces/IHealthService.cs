using smartcity.org.apis.arch.Models;

namespace smartcity.org.apis.arch.Interfaces;

public interface IHealthService
{
    HealthResponse Check();
}
