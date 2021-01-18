using AuthorizationServer.AspNetCore;
using AuthorizationServer.AspNetCore.Common.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AuthorizationServer.RunTime.Client
{
    public static class AuthorizationServerBuilder
    {
        public static IServiceCollection AddAuthorizationPermissionPolicies(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddTransient<IAuthorizationHandler, PermissionHandler>();

            return services;
        }
    }
}