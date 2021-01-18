using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Models;

namespace AuthorizationServer.Common.Interfaces
{
    public interface IRoleStore
    {
        Task AddRolesAsync(
            IEnumerable<Role> roles, 
            CancellationToken cancellationToken = default);

        Task AddRoleAsync(
            Role role,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Role>> GetRolesAsync(
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Role>> GetRolesByNameAsync(
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default);

        Task<Role> GetRoleAsync(
            string role,
            CancellationToken cancellationToken = default);
    }
}