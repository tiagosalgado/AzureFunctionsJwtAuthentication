using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };



        public static IEnumerable<ApiResource> GetApiResources()
        =>
            new List<ApiResource>
            {
                new ApiResource("azurefunction", "Azure Function Demo")
            };


        public static IEnumerable<Client> GetClients()
        =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "fx",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "azurefunction"
                    }
                }
            };
    }
}
