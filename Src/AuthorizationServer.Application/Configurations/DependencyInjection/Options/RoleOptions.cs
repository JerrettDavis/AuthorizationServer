using System.Collections.Generic;

namespace AuthorizationServer.Application.Configurations.DependencyInjection.Options
{
    public class RoleOptions
    {
        public string Name { get; set; } = null!;
        public IEnumerable<string>? PermissionsAllowed { get; set; }
        public IEnumerable<string>? PermissionsDenied { get; set; }
    }
}