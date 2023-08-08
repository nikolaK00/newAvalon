using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.UserAdministration.App.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
