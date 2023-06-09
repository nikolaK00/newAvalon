using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Abstractions;
using System.Reflection;

namespace NewAvalon.Notification.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(Notification.Boundary.AssemblyReference).Assembly
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
