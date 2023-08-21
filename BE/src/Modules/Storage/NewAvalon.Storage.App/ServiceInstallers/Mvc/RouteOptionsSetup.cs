using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace NewAvalon.Storage.App.ServiceInstallers.Mvc
{
    internal class RouteOptionsSetup : IConfigureOptions<RouteOptions>
    {
        public void Configure(RouteOptions options) => options.LowercaseUrls = true;
    }
}
