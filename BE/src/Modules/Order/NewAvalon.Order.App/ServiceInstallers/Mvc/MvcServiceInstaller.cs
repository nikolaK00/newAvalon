using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Order.App.Abstractions;
using NewAvalon.Order.App.Middlewares;

namespace NewAvalon.Order.App.ServiceInstallers.Mvc
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
                .AddApplicationPart(typeof(Order.Presentation.AssemblyReference).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<ExceptionHandlerMiddleware>();

            services.AddResponseCompression(o => o.EnableForHttps = true);
        }
    }
}
