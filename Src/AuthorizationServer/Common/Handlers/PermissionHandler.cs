using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.AspNetCore.Common.Requirements;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable once CheckNamespace
namespace AuthorizationServer.AspNetCore.Common.Handlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IAuthorizationServerRuntimeClient _client;

        public PermissionHandler(IAuthorizationServerRuntimeClient client)
        {
            _client = client;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            if (await _client.HasPermissionAsync(context.User, requirement.Name))
            {
                context.Succeed(requirement);
            }
        }
    }
}