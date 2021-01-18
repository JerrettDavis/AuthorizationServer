using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class Role
    {
        public string Name { get; set; } = null!;
        public IEnumerable<string>? PermissionsAllowed { get; set; }
        public IEnumerable<string>? PermissionsDenied { get; set; }
    }
}