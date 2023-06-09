using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Gateway.Abstractions;

namespace NewAvalon.Gateway.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
