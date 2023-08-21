using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.Notification.App.Abstractions;
using NewAvalon.Notification.Domain.Repositories;
using NewAvalon.Notification.Persistence;
using NewAvalon.Notification.Persistence.Options;
using NewAvalon.Persistence.Extensions;
using NewAvalon.Persistence.Factories;
using NewAvalon.Persistence.Relational.Interceptors;
using Scrutor;
using System.Reflection;

namespace NewAvalon.Notification.App.ServiceInstallers.Persistence
{
    public class PersistenceServiceInstaller : IServiceInstaller
    {
        private const string RepositoryPostfix = "Repository";
        private const string DataRequestPostfix = "DataRequest";

        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<NotificationDatabaseOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddNotificationDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(Notification.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(Notification.Persistence.AssemblyReference).Assembly,
                });

            AddInterceptors(services);

            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        }

        private static void AddInterceptors(IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddSingleton<ConvertDomainEventsToMessagesInterceptor>();
        }

        private static void AddNotificationDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<NotificationDbContext>((provider, builder) =>
            {
                IOptions<NotificationDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<NotificationDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Notification.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<INotificationUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<NotificationDbContext>());
        }

        private static void AddRepositories(IServiceCollection services, Assembly[] assemblies) =>
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.Where(x => x.Name.EndsWith(RepositoryPostfix)), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        private static void AddDataRequests(IServiceCollection services, Assembly[] assemblies) =>
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.Where(x => x.Name.EndsWith(DataRequestPostfix)), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Append)
                    .AsMatchingInterface()
                    .WithScopedLifetime());
    }
}
