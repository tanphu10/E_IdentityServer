using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace EMicroservice.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name="role",
                UserClaims=new List<string>
                {
                    "role"
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {

                new ApiScope("microservice_api.read","Microservices API Read Scope"),
                new ApiScope("microservice_api.write","Microservices API Write Scope"),



            };
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
        new ApiResource("microservice_api","E Microservice API")
        {
            Scopes =new List<string>
            {
                "microservice_api.read","microservice_api.write"
            },
            UserClaims=new List<string >{"role"}
        }

        };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new()
                {
                          ClientName="Microservices Swagger Client",
                          ClientId="microservices_swagger",
                          AllowedGrantTypes=GrantTypes.Implicit,
                          AllowAccessTokensViaBrowser=true,
                          RequireConsent=false,
                          AccessTokenLifetime=60*60*1,
                          RedirectUris=new List<String>()
                          {
                              "http://localhost:5001/swagger/oauth2-redireact.html"
                          },
                          PostLogoutRedirectUris=new List<String>()
                          {
                              "http://localhost:5001/swagger/oauth2-redireact.html"
                          },
                          AllowedCorsOrigins=new List<string>()
                          {
                              "http://localhost:5001"
                          },
                            AllowedScopes =
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                IdentityServerConstants.StandardScopes.Email,
                                "role",
                                "microservice_api.read",
                                "microservice_api.write"
                         }
                 },
                new()
                {
                          ClientName="Microservices Postman Client",
                          ClientId="microservices_postman",
                          Enabled=true,
                          ClientUri=null,
                          RequireClientSecret=true,
                          ClientSecrets = new[]
                            {
                              new Secret("Tanphu12345".Sha512()),

                            },
                          AllowedGrantTypes= new[]
                          {
                             GrantType.ClientCredentials,
                             GrantType.ResourceOwnerPassword

                          },
                          RequireConsent=false,
                          AccessTokenLifetime=60*60*2,
                          AllowOfflineAccess=true,
                          AllowedScopes =
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                IdentityServerConstants.StandardScopes.Email,
                                "role",
                                "microservice_api.read",
                                "microservice_api.write"
                            }

                }

        };
}
