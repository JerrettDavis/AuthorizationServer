using AuthorizationServer;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.RunTime.Client;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationServerBuilderExtensionsDefault
    {
        public static IServiceCollection AddAuthorizationServer(
            this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationServerRuntimeClient, AuthorizationServerRuntimeClient>();
            services.AddAuthorizationPermissionPolicies();

            return services;
        }
    }
}