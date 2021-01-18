using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.Common.Models;

namespace AuthorizationServer
{
    public class AuthorizationServerRuntimeClient :
        IAuthorizationServerRuntimeClient
    {
        private readonly IUserEvaluator _evaluator;

        public AuthorizationServerRuntimeClient(
            IUserEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public async Task<UserSecurityDetails?> GetUserDetailsAsync(ClaimsPrincipal user)
        {
            var sub = user.FindFirst("sub")?.Value;
            if (string.IsNullOrWhiteSpace(sub))
                return null;

            return await _evaluator.GetUserSecurityDetailsAsync(sub);
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