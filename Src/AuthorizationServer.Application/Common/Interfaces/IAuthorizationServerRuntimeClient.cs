using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Application.Common.Models;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface IAuthorizationServerRuntimeClient
    {
        Task<UserDetails?> GetUserDetailsAsync(ClaimsPrincipal user);
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission);
    }
}