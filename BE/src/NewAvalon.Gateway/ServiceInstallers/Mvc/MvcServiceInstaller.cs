using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Gateway.Abstractions;

namespace NewAvalon.Gateway.ServiceInstallers.Mvc
{
    public class MvcServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);
            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services) => services.ConfigureOptions<RouteOptionsSetup>();

        private static void InstallCore(IServiceCollection services)
        {
            services.AddRouting()
               .AddControllers();

            services.AddHttpContextAccessor();

            services.AddResponseCompression(o => o.EnableForHttps = true);
        }
    }
}
