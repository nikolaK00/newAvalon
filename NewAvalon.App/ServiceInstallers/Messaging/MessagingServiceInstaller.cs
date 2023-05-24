using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.App.Abstractions;
using NewAvalon.Catalog.Business;
using NewAvalon.Infrastructure.Messaging.Options;
using NewAvalon.Messaging.Contracts;

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
            busConfigurator.AddConsumer<ExampleConsumer>().Endpoint(e => e.Name = "example-consumer");
        }

        private static void AddRequestClients(IRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddRequestClient<IExampleRequest>();
        }
    }
}
