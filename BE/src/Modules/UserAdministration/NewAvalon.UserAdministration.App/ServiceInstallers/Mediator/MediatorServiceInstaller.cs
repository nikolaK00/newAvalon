using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Abstractions.Behaviors;
using NewAvalon.UserAdministration.App.Abstractions;
using System.Reflection;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.Mediator
{
    public class MediatorServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _businessAssemblies =
        {
            typeof(UserAdministration.Business.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services)
        {
            services.AddMediatR(_businessAssemblies);

            AddPipelineBehaviors(services);
        }

        private static void AddPipelineBehaviors(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        }
    }
}
