using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.Catalog.App.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
