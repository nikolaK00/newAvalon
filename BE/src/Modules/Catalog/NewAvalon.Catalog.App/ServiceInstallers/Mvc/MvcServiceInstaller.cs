using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Catalog.App.Abstractions;
using NewAvalon.Catalog.App.Middlewares;

namespace NewAvalon.Catalog.App.ServiceInstallers.Mvc
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
                .AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(Catalog.Presentation.AssemblyReference).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddResponseCompression(o => o.EnableForHttps = true);
        }
    }
}
