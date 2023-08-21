using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Abstractions;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
