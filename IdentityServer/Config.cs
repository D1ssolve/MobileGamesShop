using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("games", "MobileGames Server"),
            new ApiScope(name: "read", displayName: "Read your data"),
            new ApiScope(name: "write", displayName: "Write your data"),
            new ApiScope(name: "delete", displayName: "Delete your data"),
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client 
            {
                ClientId = "client",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"}
            },
            new Client
            {
                ClientId = "games",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:7260/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:7260/signout-callback-oidc"},
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "mango"
                }
            }
        };
}