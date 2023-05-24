using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewAvalon.Abstractions.Data;
using NewAvalon.App.Abstractions;
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
        }

        private static void InstallCore(IServiceCollection services)
        {
            AddUserAdministrationDbContext(services);

            AddRepositories(
                services,
                new[]
                {
                    typeof(NewAvalon.UserAdministration.Persistence.AssemblyReference).Assembly,
                });

            AddDataRequests(
                services,
                new[]
                {
                    typeof(NewAvalon.UserAdministration.Persistence.AssemblyReference).Assembly,
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
                        typeof(NewAvalon.UserAdministration.Persistence.AssemblyReference).Assembly.FullName))
                    .AddInterceptors(provider);
            });

            services.AddScoped<IUserAdministrationUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UserAdministrationDbContext>());
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
