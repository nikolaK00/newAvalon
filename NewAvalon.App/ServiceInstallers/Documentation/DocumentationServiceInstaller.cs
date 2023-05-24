using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;

namespace NewAvalon.App.ServiceInstallers.Documentation
{
    public class DocumentationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<SwaggerOptionsSetup>();

            services.ConfigureOptions<SwaggerUIOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services) => services.AddSwaggerGen();
    }
}
