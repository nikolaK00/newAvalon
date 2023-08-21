using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.Gateway.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
