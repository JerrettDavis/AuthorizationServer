using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Application.Common.Interfaces;
using AuthorizationServer.Application.Common.Models;

namespace AuthorizationServer.Application
{
    public class AuthorizationServerRuntimeClient : 
        IAuthorizationServerRuntimeClient
    {
        private readonly IUserStore _userStore;

        public AuthorizationServerRuntimeClient(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task<UserDetails?> GetUserDetailsAsync(ClaimsPrincipal user)
        {
            var sub = user.FindFirst("sub")?.Value;
            if (string.IsNullOrWhiteSpace(sub))
                return null;

            var userRecord = await _userStore.FindUserBySubjectIdAsync(sub);

            if (userRecord == null)
                return new UserDetails(new List<string>(), new List<string>());
            
            var roles = userRecord.Roles.Select(r => r.Name).ToList();
            var permissions = userRecord.GetAllowedPermission().Select(p => p.Name).ToList();

            return new UserDetails(roles, permissions);
        }

        public Task<bool> HasPermissionAsync(
            ClaimsPrincipal user, 
            string permission)
        {
            return Task.FromResult(user.Claims
                .Any(c => c.Type == "permission" && 
                          c.Value == permission));
        }
    }
}