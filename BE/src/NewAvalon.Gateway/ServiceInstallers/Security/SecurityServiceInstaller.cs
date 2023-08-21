﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NewAvalon.Gateway.Abstractions;
using System.Text;

namespace NewAvalon.Gateway.ServiceInstallers.Security
{
    public class SecurityServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services)
        {
            InstallOptions(services);

            InstallCore(services);
        }

        private static void InstallOptions(IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();
        }

        private static void InstallCore(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:5001/",
                    ValidAudience = "https://localhost:5001/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8323b408-dad9-4f3b-bdaa-ca1467eb6b93"))
                });
        }
    }
}
