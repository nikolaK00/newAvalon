﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewAvalon.App.Abstractions;
using System.Reflection;

namespace NewAvalon.App.ServiceInstallers.Validation
{
    public class ValidationServiceInstaller : IServiceInstaller
    {
        private readonly Assembly[] _boundaryAssemblies =
        {
            typeof(Order.Boundary.AssemblyReference).Assembly,
        };

        public void InstallServices(IServiceCollection services) => services.AddValidatorsFromAssemblies(_boundaryAssemblies);
    }
}
