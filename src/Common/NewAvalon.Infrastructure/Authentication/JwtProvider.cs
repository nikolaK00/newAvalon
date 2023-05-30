using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Abstractions.Services;
using NewAvalon.Authorization.Services;
using NewAvalon.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewAvalon.Infrastructure.Authentication
{
    internal sealed class JwtProvider : IJwtProvider, ITransient
    {
        private readonly ISystemTime _systemTime;
        private readonly JwtOptions _jwtOptions;
        private readonly IPermissionService _permissionService;

        public JwtProvider(ISystemTime systemTime, IOptions<JwtOptions> jwtOptions, IPermissionService permissionService)
        {
            _systemTime = systemTime;
            _jwtOptions = jwtOptions.Value;
            _permissionService = permissionService;
        }

        public string Generate(GenerateTokenRequest user)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                _systemTime.UtcNow.AddHours(3),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
