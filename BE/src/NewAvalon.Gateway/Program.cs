using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Reflection;

namespace NewAvalon.Gateway
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration(WithApplicationConfiguration)
               .UseSerilog(WithLoggerConfiguration)
               .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        private static void WithLoggerConfiguration(HostBuilderContext builderContext, LoggerConfiguration loggerConfiguration)
        {
            IConfiguration configuration = builderContext.Configuration;

            loggerConfiguration.Enrich.FromLogContext()
               .Enrich.WithThreadId()
               .MinimumLevel.Is(configuration.GetSection("Logging:LogLevel").GetValue<LogEventLevel>("Default"))
               .MinimumLevel.Override("System", configuration.GetSection("Logging:LogLevel").GetValue<LogEventLevel>("System"))
               .MinimumLevel.Override("Microsoft", configuration.GetSection("Logging:LogLevel").GetValue<LogEventLevel>("Microsoft"))
               .MinimumLevel.Override("Microsoft.Hosting.Lifetime", configuration.GetSection("Logging:LogLevel").GetValue<LogEventLevel>("Microsoft.Hosting.Lifetime"))
               .WriteTo.Console(new CompactJsonFormatter());
        }

        private static void WithApplicationConfiguration(HostBuilderContext builderContext, IConfigurationBuilder builder)
        {
            IHostEnvironment environment = builderContext.HostingEnvironment;
            builder.SetBasePath(environment.ContentRootPath);
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);
            builder.AddJsonFile("logger.settings.json", false, true);
            builder.AddJsonFile($"logger.settings.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);
            builder.AddJsonFile("ocelot.json", false, true);
            builder.AddJsonFile($"ocelot.{environment.EnvironmentName.ToLowerInvariant()}.json", true, true);

            if (!environment.IsProduction())
            {
                builder.AddUserSecrets(Assembly.GetExecutingAssembly());
            }

            builder.AddEnvironmentVariables();
        }
    }
}
