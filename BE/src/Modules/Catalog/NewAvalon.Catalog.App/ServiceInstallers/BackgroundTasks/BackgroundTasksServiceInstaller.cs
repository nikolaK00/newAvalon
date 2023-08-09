using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Catalog.App.Abstractions;
using NewAvalon.Catalog.App.ServiceInstallers.BackgroundTasks.Catalog;
using Quartz;

namespace NewAvalon.Catalog.App.ServiceInstallers.BackgroundTasks
{
    public sealed class BackgroundTasksServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<QuartzHostedServiceOptionsSetup>();

            services.ConfigureOptions<CatalogPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<CatalogPublishDomainEventsJobSetup>();
        }

        public void InstallCore(IServiceCollection services)
        {
            services.AddQuartz(configurator => configurator.UseMicrosoftDependencyInjectionJobFactory());

            services.AddQuartzHostedService();
        }
    }
}
