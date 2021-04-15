using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Toto.IdentityProvider
{
    public static class Config
    {
        public static IList<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "6E941E62-A592-4493-83EA-4E9CE7CED1BA",
                    Username = "Toto",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Thorsten"),
                        new Claim("family_name", "Lenzen") 
                    }
                },
                new TestUser
                {
                    SubjectId = "7E7D0398-A1F5-425D-8357-68229B867655",
                    Username = "Sigi",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Sigrid"),
                        new Claim("family_name", "Nüsing-Lenzen") 
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}