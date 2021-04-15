using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Toto.IdentityProvider
{
    public class User
    {
        public User()
        {
            this.Claims = new List<Claim>();
        }
        
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}