﻿using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Abstractions;
using Scrutor;
using System.Reflection;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.Domain
{
    public class DomainServiceInstaller : IServiceInstaller
    {
        private const string FactoryPostfix = "Factory";

        public void InstallServices(IServiceCollection services) =>
            AddFactories(services, new[]
            {
                typeof(UserAdministration.Domain.AssemblyReference).Assembly,
            });

        private static void AddFactories(IServiceCollection services, Assembly[] assemblies) =>
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.Where(x => x.Name.EndsWith(FactoryPostfix)), false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithScopedLifetime());
    }
}
