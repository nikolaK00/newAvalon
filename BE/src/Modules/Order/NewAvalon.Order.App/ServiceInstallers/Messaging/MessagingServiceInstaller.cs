using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Infrastructure.Messaging.Options;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.Messaging.Contracts.Permissions;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Order.App.Abstractions;
using NewAvalon.Order.Business.Orders.Consumers;

namespace NewAvalon.Order.App.ServiceInstallers.Messaging
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
            busConfigurator.AddConsumer<IsProductUsedRequestConsumer>().Endpoint(e => e.Name = "is-product-used-request-consumer");
        }

        private static void AddRequestClients(IRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddRequestClient<IUserDetailsRequest>();

            busConfigurator.AddRequestClient<IGetPermissionsRequest>();

            busConfigurator.AddRequestClient<IUserDetailsListRequest>();

            busConfigurator.AddRequestClient<IImageRequest>();

            busConfigurator.AddRequestClient<IFileListRequest>();

            busConfigurator.AddRequestClient<IUploadFileRequest>();

            busConfigurator.AddRequestClient<IStoreUploadedFileRequest>();

            busConfigurator.AddRequestClient<IGetCatalogProductListRequest>();

            busConfigurator.AddRequestClient<IIsProductUsedRequest>();
        }
    }
}
