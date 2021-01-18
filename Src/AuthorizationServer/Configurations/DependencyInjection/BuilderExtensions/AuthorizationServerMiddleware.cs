using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace AuthorizationServer.RunTime.Client.AspNetCore
{
    public class AuthorizationServerMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(
            HttpContext context, 
            IAuthorizationServerRuntimeClient client)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userDetails = await client.GetUserDetailsAsync(context.User);

                if (userDetails != null)
                {
                    var roles = userDetails.Roles.Select(x => new Claim("role", x));
                    var permissions = userDetails.Permissions
                        .Select(x => new Claim("permission", x));

                    var id = new ClaimsIdentity(
                        "AuthorizationServerMiddleware", 
                        "name", 
                        "role");
                    id.AddClaims(roles);
                    id.AddClaims(permissions);

                    context.User.AddIdentity(id);    
                }
            }

            await _next(context);
        }
    }
}