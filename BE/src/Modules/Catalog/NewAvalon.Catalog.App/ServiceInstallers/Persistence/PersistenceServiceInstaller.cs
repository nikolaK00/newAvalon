using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.Catalog.App.Abstractions;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Catalog.Persistence;
using NewAvalon.Catalog.Persistence.Options;
using NewAvalon.Persistence.Extensions;
using NewAvalon.Persistence.Factories;
using NewAvalon.Persistence.Relational.Interceptors;
using Scrutor;
using System.Reflection;

namespace NewAvalon.Catalog.App.ServiceInstallers.Persistence
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
            services.ConfigureOptions<CatalogDatabaseOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddCatalogDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(Catalog.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(Catalog.Persistence.AssemblyReference).Assembly,
                });

            AddInterceptors(services);

            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        }

        private static void AddInterceptors(IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddSingleton<ConvertDomainEventsToMessagesInterceptor>();
        }

        private static void AddCatalogDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<CatalogDbContext>((provider, builder) =>
            {
                IOptions<CatalogDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<CatalogDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(Catalog.Persistence.AssemblyReference).Assembly.FullName))
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
