using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.Order.App.Abstractions;
using NewAvalon.Order.Domain.Repositories;
using NewAvalon.Order.Persistence;
using NewAvalon.Order.Persistence.Options;
using NewAvalon.Persistence.Extensions;
using NewAvalon.Persistence.Factories;
using NewAvalon.Persistence.Relational.Interceptors;
using Scrutor;
using System.Reflection;

namespace NewAvalon.Order.App.ServiceInstallers.Persistence
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
            services.ConfigureOptions<OrderDatabaseOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddOrderDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(Order.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(Order.Persistence.AssemblyReference).Assembly,
                });

            AddInterceptors(services);

            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        }

        private static void AddInterceptors(IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddSingleton<ConvertDomainEventsToMessagesInterceptor>();
        }

        private static void AddOrderDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<OrderDbContext>((provider, builder) =>
            {
                IOptions<OrderDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<OrderDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Order.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<IOrderUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<OrderDbContext>());
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
