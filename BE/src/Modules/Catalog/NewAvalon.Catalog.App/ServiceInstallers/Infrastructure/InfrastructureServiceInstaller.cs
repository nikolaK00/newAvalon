using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Catalog.App.Abstractions;
using NewAvalon.Catalog.App.Extensions;

namespace NewAvalon.Catalog.App.ServiceInstallers.Infrastructure
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.AddTransientServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(NewAvalon.Infrastructure.AssemblyReference).Assembly);

            services.AddScopedServicesAsMatchingInterfaces(typeof(Catalog.Persistence.AssemblyReference).Assembly);
        }
    }
}
