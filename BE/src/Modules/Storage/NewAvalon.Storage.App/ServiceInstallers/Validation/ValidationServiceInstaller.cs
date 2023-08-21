using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Storage.App.Abstractions;
using System.Reflection;

namespace NewAvalon.Storage.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(NewAvalon.Storage.Boundary.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
