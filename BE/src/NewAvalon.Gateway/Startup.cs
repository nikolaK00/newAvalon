using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewAvalon.Gateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace NewAvalon.Gateway
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesFromAssembly(GetType().Assembly);

            services.AddOcelot(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            if (env.IsDevelopment() || env.IsEnvironment("Debug"))
            {
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseOcelot().Wait();
        }
    }
}
