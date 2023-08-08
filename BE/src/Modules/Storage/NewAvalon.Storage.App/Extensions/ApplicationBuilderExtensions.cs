using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Storage.App.Middlewares;
using NewAvalon.Storage.Persistence;

namespace NewAvalon.Storage.App.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();

            ApplyStorageMigrations(scope);
        }

        private static void ApplyStorageMigrations(IServiceScope scope)
        {
            using StorageDbContext notificationDbContext =
                scope.ServiceProvider.GetRequiredService<StorageDbContext>();

            notificationDbContext.Database.Migrate();
        }
    }
}
