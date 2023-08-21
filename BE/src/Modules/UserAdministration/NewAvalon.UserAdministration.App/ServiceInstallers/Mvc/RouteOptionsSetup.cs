﻿using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.Mvc
{
    internal class RouteOptionsSetup : IConfigureOptions<RouteOptions>
    {
        public void Configure(RouteOptions options) => options.LowercaseUrls = true;
    }
}
