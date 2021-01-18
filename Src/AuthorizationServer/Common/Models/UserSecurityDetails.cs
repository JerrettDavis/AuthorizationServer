using System.Collections.Generic;

namespace AuthorizationServer.Common.Models
{
    public class UserSecurityDetails
    {
        public UserSecurityDetails()
        {
            Roles = new List<string>();
            Permissions = new List<string>();
        }
        public UserSecurityDetails(
            IEnumerable<string> roles, 
            IEnumerable<string> permissions)
        {
            Roles = roles;
            Permissions = permissions;
        }
        
        public IEnumerable<string> Roles { get; init; }
        public IEnumerable<string> Permissions { get; init; }
    }
}