using AuthorizationServer.RunTime.Client.AspNetCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAuthorizationServerClaims(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationServerMiddleware>();
        }
    }
}