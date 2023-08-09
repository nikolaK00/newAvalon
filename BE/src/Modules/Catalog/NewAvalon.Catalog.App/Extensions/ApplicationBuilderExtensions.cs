using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Catalog.App.Middlewares;
using NewAvalon.Catalog.Persistence;

namespace NewAvalon.Catalog.App.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();

            ApplyCatalogMigrations(scope);
        }

        private static void ApplyCatalogMigrations(IServiceScope scope)
        {
            using CatalogDbContext catalogDbContext =
                scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

            catalogDbContext.Database.Migrate();
        }
    }
}
