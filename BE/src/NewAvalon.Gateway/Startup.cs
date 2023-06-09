using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewAvalon.Gateway.Extensions;
using Ocelot.Middleware;
using System.IO;

namespace NewAvalon.Gateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.InstallServicesFromAssembly(GetType().Assembly);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // TODO return this line inside if block "env.IsDevelopment()" when we have proper environment set
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment() || env.IsEnvironment("Debug"))
            {
                // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();

            app.Map("/swagger/v1/swagger.json", b => b.Run(async x =>
            {
                string json = await File.ReadAllTextAsync("swagger.json");

                await x.Response.WriteAsync(json);
            }));

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway"));

            app.UseHealthChecks("/health");

            app.UseOcelot().Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
