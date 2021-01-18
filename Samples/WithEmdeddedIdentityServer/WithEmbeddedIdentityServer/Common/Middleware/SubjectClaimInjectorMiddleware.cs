using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WithEmbeddedIdentityServer.Common.Middleware
{
    public class SubjectClaimInjectorMiddleware
    {
        private readonly RequestDelegate _next;

        public SubjectClaimInjectorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(
            HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var user = context.User;
                if (string.IsNullOrWhiteSpace(user.FindFirstValue("sub")))
                {
                    var claim = new Claim("sub", user.FindFirstValue(ClaimTypes.NameIdentifier));
                    var id = new ClaimsIdentity(
                        "SubInjectorMiddleware",
                        "name",
                        "role");
                    id.AddClaim(claim);
                    
                    context.User.AddIdentity(id);
                }
            }

            await _next(context);
        }
    }
}