using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.App.Abstractions;
using NewAvalon.Notification.Infrastructure.Options;

namespace NewAvalon.App.ServiceInstallers.Notifications
{
    public class NotificationsServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services) =>
            services.ConfigureOptions<SendEmailNotificationJobOptionsSetup>();

        private static void InstallCore(IServiceCollection services)
        {
            A
            services.AddMandrill((serviceProvider, options) =>
            {
                IOptions<EmailNotificationsJobOptions> configuration =
                    serviceProvider.GetRequiredService<IOptions<EmailNotificationsJobOptions>>();

                options.ApiKey = configuration.Value.ApiKey;
            });
        }
    }
}
