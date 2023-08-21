using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.Storage.App.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
