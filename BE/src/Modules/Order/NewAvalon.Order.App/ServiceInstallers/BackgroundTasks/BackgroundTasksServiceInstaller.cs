using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Order.App.Abstractions;
using NewAvalon.Order.App.ServiceInstallers.BackgroundTasks.Order;
using Quartz;

namespace NewAvalon.Order.App.ServiceInstallers.BackgroundTasks
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

            services.ConfigureOptions<OrderPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<OrderPublishDomainEventsJobSetup>();

            services.ConfigureOptions<OrderDeliveryJobSetup>();
        }

        public void InstallCore(IServiceCollection services)
        {
            services.AddQuartz(configurator => configurator.UseMicrosoftDependencyInjectionJobFactory());

            services.AddQuartzHostedService();
        }
    }
}
