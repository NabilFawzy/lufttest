using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResource
        {
            Name = "profile",
            DisplayName = "User profile",
            UserClaims = { "name", "email", "preferred_username" }
        }
    };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[] { new ApiScope("api1", "My Protected API") };

        public static IEnumerable<ApiResource> ApiResources =>
    new List<ApiResource>
    {
        new ApiResource("api1", "My Protected API")
        {
            Scopes = { "api1" }
        }
    };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            new Client
            {
                ClientId = "ecommerce",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris = { "http://localhost:4200/callback" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowedScopes = { "openid", "profile", "api1" },
                AllowAccessTokensViaBrowser = true,
                AlwaysIncludeUserClaimsInIdToken = true
            }
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
            new TestUser
            {
                SubjectId = "1",
                Username = "nabil",
                Password = "pass123",
                Claims = new List<Claim>
                {
                    new Claim("name", "Nabil Fawzy"),
                    new Claim("preferred_username", "nabil"),
                    new Claim("email", "nabil@example.com")
                }
            }
            };
    }
    }
