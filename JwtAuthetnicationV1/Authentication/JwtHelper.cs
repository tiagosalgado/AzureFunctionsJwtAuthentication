using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace JwtAuthetnicationV1.Authentication
{
    public static class JwtHelper
    {
        const string Issuer = "http://localhost:5000";
        const string Audience = "azurefunction";
        static readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        static JwtHelper()
        {
            _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                metadataAddress: $"{Issuer}/.well-known/openid-configuration",
                configRetriever: new OpenIdConnectConfigurationRetriever(),
                docRetriever: new HttpDocumentRetriever() { RequireHttps = false }
            );
        }

        public static async Task<ClaimsPrincipal> ValidateToken(AuthenticationHeaderValue token)
        {
            var config = await _configurationManager.GetConfigurationAsync(CancellationToken.None);
            var validationParameters = new TokenValidationParameters
            {
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = false,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKeys = config.SigningKeys

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var cp = tokenHandler.ValidateToken(
                token: token.Parameter,
                validationParameters: validationParameters,
                validatedToken: out SecurityToken validatedToken);

            return cp;
        }
    }
}
