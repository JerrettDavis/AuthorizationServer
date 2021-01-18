using System.Threading.Tasks;
using AuthorizationServer.AspNetCore.Common.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace AuthorizationServer.AspNetCore
{
    public class AuthorizationPolicyProvider :
        DefaultAuthorizationPolicyProvider
    {
        public AuthorizationPolicyProvider(
            IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(
            string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName) ??
                         new AuthorizationPolicyBuilder()
                             .AddRequirements(new PermissionRequirement(policyName))
                             .Build();

            return policy;
        }
    }
}