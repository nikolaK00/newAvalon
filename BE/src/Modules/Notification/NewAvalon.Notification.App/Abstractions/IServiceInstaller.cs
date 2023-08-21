using Microsoft.Extensions.DependencyInjection;

namespace NewAvalon.Notification.App.Abstractions
{
    internal interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
