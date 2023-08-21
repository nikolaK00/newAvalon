using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Abstractions;
using NewAvalon.Notification.App.ServiceInstallers.BackgroundTasks.Notifications;
using NewAvalon.Notification.App.ServiceInstallers.Notifications;
using Quartz;

namespace NewAvalon.Notification.App.ServiceInstallers.BackgroundTasks
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

            services.ConfigureOptions<SendEmailNotificationJobOptionsSetup>();

            services.ConfigureOptions<SendEmailNotificationJobSetup>();
        }

        public void InstallCore(IServiceCollection services)
        {
            services.AddQuartz(configurator => configurator.UseMicrosoftDependencyInjectionJobFactory());

            services.AddQuartzHostedService();
        }
    }
}
