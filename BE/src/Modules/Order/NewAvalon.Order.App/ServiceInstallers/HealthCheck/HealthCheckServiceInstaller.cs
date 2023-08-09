using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Order.App.Abstractions;

namespace NewAvalon.Order.App.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
