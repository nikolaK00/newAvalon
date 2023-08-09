using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Catalog.App.Abstractions;
using System.Reflection;

namespace NewAvalon.Catalog.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(Catalog.Boundary.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
