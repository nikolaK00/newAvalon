using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Middlewares;
using NewAvalon.Order.Persistence;

namespace NewAvalon.App.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();

            ApplyOrderMigrations(scope);
        }

        private static void ApplyOrderMigrations(IServiceScope scope)
        {
            using OrderDbContext orderDbContext =
                scope.ServiceProvider.GetRequiredService<OrderDbContext>();

            orderDbContext.Database.Migrate();
        }
    }
}
