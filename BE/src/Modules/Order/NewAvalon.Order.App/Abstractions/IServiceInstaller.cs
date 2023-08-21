using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.Order.App.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
