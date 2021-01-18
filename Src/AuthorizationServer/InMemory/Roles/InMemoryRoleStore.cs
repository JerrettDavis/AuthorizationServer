using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.Models;

namespace AuthorizationServer.InMemory.Roles
{
    public class InMemoryRoleStore : IRoleStore
    {
        private readonly IEnumerable<Role> _roles;

        public InMemoryRoleStore(IEnumerable<Role> roles)
        {
            _roles = roles;
        }

        public Task<IEnumerable<Role>> GetRolesByNameAsync(
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_roles
                .Where(r => roles.Contains(r.Name)));
        }
    }
}