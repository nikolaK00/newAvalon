using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Abstractions;

namespace NewAvalon.Notification.App.ServiceInstallers.HealthCheck
{
    public class HealthCheckServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services) => services.AddHealthChecks();
    }
}
