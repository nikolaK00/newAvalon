using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Gateway.Abstractions;
using System.Linq;
using System.Reflection;

namespace NewAvalon.Gateway.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void InstallServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var serviceInstallers = ServiceInstallerFactory.GetServiceInstallersFromAssembly(assembly).ToList();

            serviceInstallers.ForEach(x => x.InstallServices(services));
        }
    }
}
