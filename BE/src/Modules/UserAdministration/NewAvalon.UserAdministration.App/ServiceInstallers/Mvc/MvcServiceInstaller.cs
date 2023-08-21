using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Abstractions;
using NewAvalon.UserAdministration.App.Middlewares;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.Mvc
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
                .AddApplicationPart(typeof(UserAdministration.Presentation.AssemblyReference).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddResponseCompression(o => o.EnableForHttps = true);
        }
    }
}
