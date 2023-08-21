using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Abstractions;
using NewAvalon.UserAdministration.App.ServiceInstallers.BackgroundTasks.UserAdministration;
using Quartz;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.BackgroundTasks
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

            services.ConfigureOptions<UserAdministrationPublishDomainEventsJobOptionsSetup>();

            services.ConfigureOptions<UserAdministrationPublishDomainEventsJobSetup>();
        }

        public void InstallCore(IServiceCollection services)
        {
            services.AddQuartz(configurator => configurator.UseMicrosoftDependencyInjectionJobFactory());

            services.AddQuartzHostedService();
        }
    }
}
