using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Abstractions;
using Scrutor;
using System.Reflection;

namespace NewAvalon.Notification.App.ServiceInstallers.Domain
{
    public class DomainServiceInstaller : IServiceInstaller
    {
        private const string FactoryPostfix = "Factory";

        public void InstallServices(IServiceCollection services) =>
            AddFactories(services, new[]
            {
                    typeof(Notification.Domain.AssemblyReference).Assembly,
            });

        private static void AddFactories(IServiceCollection services, Assembly[] assemblies) =>
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.Where(x => x.Name.EndsWith(FactoryPostfix)), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithScopedLifetime());
    }
}
