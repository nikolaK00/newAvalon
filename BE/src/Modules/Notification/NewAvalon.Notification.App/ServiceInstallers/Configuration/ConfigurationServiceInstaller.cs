using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Abstractions;

namespace NewAvalon.Notification.App.ServiceInstallers.Configuration
{
    public class ConfigurationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.ConfigureOptions<EmailTemplatesOptionsSetup>();
        }
    }
}
