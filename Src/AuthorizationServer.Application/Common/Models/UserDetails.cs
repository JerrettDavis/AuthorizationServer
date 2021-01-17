using System.Collections.Generic;

namespace AuthorizationServer.Application.Common.Models
{
    public class UserDetails
    {
        public UserDetails(
            IEnumerable<string> roles, 
            IEnumerable<string> permissions)
        {
            Roles = roles;
            Permissions = permissions;
        }
        
        public IEnumerable<string> Roles { get; }
        public IEnumerable<string> Permissions { get; }
    }
}