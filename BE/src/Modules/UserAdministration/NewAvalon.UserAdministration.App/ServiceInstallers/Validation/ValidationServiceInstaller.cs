using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Abstractions;
using System.Reflection;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(UserAdministration.Boundary.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
