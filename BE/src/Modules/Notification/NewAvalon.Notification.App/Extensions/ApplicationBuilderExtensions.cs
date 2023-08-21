using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.Notification.App.Middlewares;
using NewAvalon.Notification.Persistence;

namespace NewAvalon.Notification.App.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();

            ApplyNotificationMigrations(scope);
        }

        private static void ApplyNotificationMigrations(IServiceScope scope)
        {
            using NotificationDbContext notificationDbContext =
                scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

            notificationDbContext.Database.Migrate();
        }
    }
}
