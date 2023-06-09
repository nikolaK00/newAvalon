using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace NewAvalon.Gateway.ServiceInstallers.Mvc
{
    internal class RouteOptionsSetup : IConfigureOptions<RouteOptions>
    {
        public void Configure(RouteOptions options) => options.LowercaseUrls = true;
    }
}
