using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.Catalog;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.Order;
using NewAvalon.App.ServiceInstallers.BackgroundTasks.UserAdministration;
using Quartz;

namespace NewAvalon.App.ServiceInstallers.BackgroundTasks
{
    public sealed class BackgroundTasksServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            services.ConfigureOptions<QuartzHostedServiceOptionsSetup>();

            services.ConfigureOptions<UserAdministrationPublishDomainEventsJobSetup>();

            services.ConfigureOptions<UserAdministrationPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<OrderPublishDomainEventsJobSetup>();

            services.ConfigureOptions<OrderPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<CatalogPublishDomainEventsJobSetup>();

            services.ConfigureOptions<CatalogPublishDomainEventsJobOptionsSetup>();

            services.AddQuartz(configurator => configurator.UseMicrosoftDependencyInjectionJobFactory());

            services.AddQuartzHostedService();
        }
    }
}
