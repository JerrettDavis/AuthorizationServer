using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.EntityFramework.Common.Interfaces;
using AuthorizationServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthorizationServer.EntityFramework.Stores
{
    public class RoleStore : IRoleStore
    {
        protected readonly IAuthorizationDbContext Context;
        protected readonly ILogger<RoleStore> Logger;

        public RoleStore(
            IAuthorizationDbContext context, 
            ILogger<RoleStore> logger)
        {
            Context = context;
            Logger = logger;
        }

        public async Task<IEnumerable<Role>> GetRolesByNameAsync(
            IEnumerable<string> roles, 
            CancellationToken cancellationToken = default)
        {
            return await Context.Roles
                .Include(r => r.PermissionsAllowed)
                .Include(r => r.PermissionsDenied)
                .Where(r => roles.Contains(r.Name))
                .Select(r => new Role
                {
                    Name = r.Name,
                    PermissionsAllowed = r.PermissionsAllowed.Select(p => p.Name),
                    PermissionsDenied = r.PermissionsDenied.Select(p => p.Name)
                })
                .ToListAsync(cancellationToken);
        }
    }
}