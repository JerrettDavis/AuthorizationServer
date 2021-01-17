using System.Collections.Generic;
using AuthorizationServer.Domain.Models;

namespace AuthorizationServer.Application.Configurations.DependencyInjection.Options
{
    public class AuthorizationServerOptions
    {
        public IEnumerable<string> Permissions { get; set; } = null!;
        public IEnumerable<UserOptions> Users { get; set; } = null!;
        public IEnumerable<Role> Roles { get; set; } = null!;
    }
}