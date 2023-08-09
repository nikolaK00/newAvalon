using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Catalog.App.Abstractions;

namespace NewAvalon.Catalog.App.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
