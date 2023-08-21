﻿using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Catalog.App.Abstractions;
using NewAvalon.Catalog.Business.Products.Consumers.GetCatalogProductListRequest;
using NewAvalon.Catalog.Business.Products.Consumers.ImageDeletedEvent;
using NewAvalon.Catalog.Business.Products.Consumers.OrderDeletedEvent;
using NewAvalon.Catalog.Business.Products.Consumers.ProductAddedEvent;
using NewAvalon.Infrastructure.Messaging.Options;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.Messaging.Contracts.Permissions;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Messaging.Contracts.Users;

namespace NewAvalon.Catalog.App.ServiceInstallers.Messaging
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
            busConfigurator.AddConsumer<ProductImageDeletedEventConsumer>().Endpoint(e => e.Name = "product-image-deleted-event-consumer");

            busConfigurator.AddConsumer<ProductAddedEventConsumer>().Endpoint(e => e.Name = "product-added-event-consumer");

            busConfigurator.AddConsumer<GetCatalogProductListRequestConsumer>().Endpoint(e => e.Name = "get-catalog-product-list-request-consumer");

            busConfigurator.AddConsumer<OrderDeletedEventConsumer>().Endpoint(e => e.Name = "order-deleted-event-consumer");
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