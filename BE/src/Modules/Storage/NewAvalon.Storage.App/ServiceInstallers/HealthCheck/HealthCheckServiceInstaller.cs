using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Storage.App.Abstractions;

namespace NewAvalon.Storage.App.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
