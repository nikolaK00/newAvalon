using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.App.Abstractions;
using NewAvalon.Infrastructure.Messaging.Options;
using NewAvalon.Messaging.Contracts.Permissions;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Notification.Business.Notifications.Consumers;
using NewAvalon.UserAdministration.Business.Permissions.Consumers;
using NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsListRequest;
using NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsRequest;

namespace NewAvalon.App.ServiceInstallers.Messaging
{
    public class MessagingServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services) => services.ConfigureOptions<MessageBrokerOptionsSetup>();

        private static void InstallCore(IServiceCollection services)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                AddConsumers(busConfigurator);

                AddRequestClients(busConfigurator);

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerOptions messageBrokerOptions =
                        context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;

                    //configurator.Host(new Uri(messageBrokerOptions.Host), h =>
                    //{
                    //    h.Username(messageBrokerOptions.Username);
                    //    h.Password(messageBrokerOptions.Password);
                    //});

                    configurator.Host(messageBrokerOptions.Host);

                    configurator.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService();
        }

        private static void AddConsumers(IRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddConsumer<UserDetailsRequestConsumer>().Endpoint(e => e.Name = "user-details-request-consumer");

            busConfigurator.AddConsumer<GetPermissionsRequestConsumer>().Endpoint(e => e.Name = "user-administration-get-permissions-request");

            busConfigurator.AddConsumer<NotificationCreatedEventConsumer>().Endpoint(e => e.Name = "notification-created-event-consumer");

            busConfigurator.AddConsumer<UserDetailsListRequestConsumer>().Endpoint(e => e.Name = "user-details-list-request-consumer");
        }

        private static void AddRequestClients(IRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddRequestClient<IUserDetailsRequest>();

            busConfigurator.AddRequestClient<IGetPermissionsRequest>();

            busConfigurator.AddRequestClient<IUserDetailsListRequest>();
        }
    }
}
