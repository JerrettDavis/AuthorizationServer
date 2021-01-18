using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Common.Models;

namespace AuthorizationServer.Common.Interfaces
{
    public interface IAuthorizationServerRuntimeClient
    {
        Task<UserSecurityDetails?> GetUserDetailsAsync(ClaimsPrincipal user);
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission);
    }
}