using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Abstractions;
using NewAvalon.UserAdministration.App.Extensions;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.Infrastructure
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddTransientServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);
        }
    }
}
