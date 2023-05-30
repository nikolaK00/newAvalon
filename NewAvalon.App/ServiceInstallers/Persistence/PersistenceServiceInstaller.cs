using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.App.Abstractions;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Catalog.Persistence;
using NewAvalon.Catalog.Persistence.Options;
using NewAvalon.Order.Persistence;
using NewAvalon.Order.Persistence.Options;
using NewAvalon.Persistence.Extensions;
using NewAvalon.Persistence.Factories;
using NewAvalon.Persistence.Relational.Interceptors;
using NewAvalon.UserAdministration.Domain.Repositories;
using NewAvalon.UserAdministration.Persistence;
using NewAvalon.UserAdministration.Persistence.Options;
using Scrutor;
using System.Reflection;

namespace NewAvalon.App.ServiceInstallers.Persistence
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
            services.ConfigureOptions<UserAdministrationDatabaseOptionsSetup>();
            services.ConfigureOptions<CatalogDatabaseOptionsSetup>();
            services.ConfigureOptions<OrderDatabaseOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddUserAdministrationDbContext(services);

            AddCatalogDbContext(services);

            AddOrderDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(UserAdministration.Persistence.AssemblyReference).Assembly,
                    typeof(Catalog.Persistence.AssemblyReference).Assembly,
                    typeof(Order.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(UserAdministration.Persistence.AssemblyReference).Assembly,
                    typeof(Catalog.Persistence.AssemblyReference).Assembly,
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

        private static void AddUserAdministrationDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<UserAdministrationDbContext>((provider, builder) =>
            {
                IOptions<UserAdministrationDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<UserAdministrationDatabaseOptions>>();

                builder.UseNpgsql("Host=localhost; Database=user_administration; Username=postgres; Password=postgres",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(
                        typeof(UserAdministration.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<IUserAdministrationUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UserAdministrationDbContext>());
        }

        private static void AddCatalogDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<CatalogDbContext>((provider, builder) =>
            {
                IOptions<CatalogDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<CatalogDatabaseOptions>>();

                builder.UseNpgsql("Host=localhost; Database=catalog; Username=postgres; Password=postgres",
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Catalog.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<ICatalogUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CatalogDbContext>());
        }

        private static void AddOrderDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<OrderDbContext>((provider, builder) =>
            {
                IOptions<OrderDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<OrderDatabaseOptions>>();

                builder.UseNpgsql("Host=localhost; Database=order; Username=postgres; Password=postgres",
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Order.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<ICatalogUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CatalogDbContext>());
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
