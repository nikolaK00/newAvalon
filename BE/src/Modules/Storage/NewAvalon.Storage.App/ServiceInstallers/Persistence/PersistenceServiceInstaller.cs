using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.Persistence.Extensions;
using NewAvalon.Persistence.Factories;
using NewAvalon.Persistence.Relational.Interceptors;
using NewAvalon.Storage.App.Abstractions;
using NewAvalon.Storage.Domain.Repositories;
using NewAvalon.Storage.Persistence;
using NewAvalon.Storage.Persistence.Options;
using Scrutor;
using System.Reflection;

namespace NewAvalon.Storage.App.ServiceInstallers.Persistence
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
            services.ConfigureOptions<StorageDatabaseOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddStorageDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(NewAvalon.Storage.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(NewAvalon.Storage.Persistence.AssemblyReference).Assembly,
                });

            AddInterceptors(services);

            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        }

        private static void AddInterceptors(IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddSingleton<ConvertDomainEventsToMessagesInterceptor>();
        }

        private static void AddStorageDbContext(IServiceCollection services)
        {
            services.AddDbContextPool<StorageDbContext>((provider, builder) =>
            {
                IOptions<StorageDatabaseOptions> dbSettingsOptions =
                    provider.GetRequiredService<IOptions<StorageDatabaseOptions>>();

                builder.UseNpgsql(dbSettingsOptions.Value.GetConnectionString(),
                        optionsBuilder => optionsBuilder.MigrationsAssembly(
                            typeof(NewAvalon.Storage.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<IStorageUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<StorageDbContext>());
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
