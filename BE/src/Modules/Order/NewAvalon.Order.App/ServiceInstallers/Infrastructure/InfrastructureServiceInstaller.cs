using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Order.App.Abstractions;
using NewAvalon.Order.App.Extensions;

namespace NewAvalon.Order.App.ServiceInstallers.Infrastructure
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
