using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Storage.App.Abstractions;
using NewAvalon.Storage.App.Middlewares;

namespace NewAvalon.Storage.App.ServiceInstallers.Mvc
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
                .AddApplicationPart(typeof(NewAvalon.Storage.Presentation.AssemblyReference).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddResponseCompression(o => o.EnableForHttps = true);
        }
    }
}
