using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewAvalon.Gateway.Properties;

namespace NewAvalon.Gateway.ServiceInstallers.Security
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string ConfigurationSectionName = "Jwt";

        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(JwtOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
