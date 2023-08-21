using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.UserAdministration.App.Middlewares;
using NewAvalon.UserAdministration.Persistence;

namespace NewAvalon.UserAdministration.App.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();

            ApplyUserAdministrationMigrations(scope);
        }

        private static void ApplyUserAdministrationMigrations(IServiceScope scope)
        {
            using UserAdministrationDbContext userAdministrationDbContext =
                scope.ServiceProvider.GetRequiredService<UserAdministrationDbContext>();

            userAdministrationDbContext.Database.Migrate();
        }
    }
}
