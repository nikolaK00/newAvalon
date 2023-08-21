using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Abstractions;
using NewAvalon.Notification.App.Extensions;

namespace NewAvalon.Notification.App.ServiceInstallers.Infrastructure
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddTransientServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddTransientServicesAsMatchingInterfaces(typeof(Notification.Infrastructure.AssemblyReference).Assembly);
        }
    }
}
