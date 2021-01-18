using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<string, Role> _roles;

        public InMemoryRoleStore(IEnumerable<Role> roles)
        {
            _roles = new ConcurrentDictionary<string, Role>(roles.ToDictionary(r => r.Name));
        }
        
        public Task AddRolesAsync(
            IEnumerable<Role> roles, 
            CancellationToken cancellationToken = default)
        {
            var tasks = roles.Select(async role => 
                await AddRoleAsync(role, cancellationToken));
            
            return Task.WhenAll(tasks);
        }

        public Task AddRoleAsync(
            Role role, 
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_roles.AddOrUpdate(role.Name, role, (_, v) => v));
        }

        public Task<IEnumerable<Role>> GetRolesAsync(
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_roles.ToArray().Select(r => r.Value));
        }

        public Task<IEnumerable<Role>> GetRolesByNameAsync(
            IEnumerable<string> roles, 
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_roles
                .Where(r => roles.Contains(r.Key))
                .Select(r => r.Value));
        }

        public Task<Role> GetRoleAsync(
            string role, 
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_roles[role]);
        }
    }
}