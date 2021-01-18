using System.Collections.Generic;

namespace AuthorizationServer.Common.Models
{
    public class UserSecurityDetails
    {
        public UserSecurityDetails(
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