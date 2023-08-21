using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Storage.App.Abstractions;
using NewAvalon.Storage.App.Extensions;

namespace NewAvalon.Storage.App.ServiceInstallers.Infrastructure
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
