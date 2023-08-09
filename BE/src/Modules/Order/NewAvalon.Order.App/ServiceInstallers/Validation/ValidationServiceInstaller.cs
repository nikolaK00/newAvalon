using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Order.App.Abstractions;
using System.Reflection;

namespace NewAvalon.Order.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(Order.Boundary.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
