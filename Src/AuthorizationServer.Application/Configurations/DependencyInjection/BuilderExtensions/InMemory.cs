using AuthorizationServer.Application;
using AuthorizationServer.Application.Common.Interfaces;
using AuthorizationServer.Application.InMemory.Configuration;
using AuthorizationServer.Application.InMemory.Users;
using AuthorizationServer.RunTime.Client;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationServerBuilderExtensionsInMemory
    {
        public static IServiceCollection AddInMemoryAuthorizationServer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var mapper = new ConfigurationMapper(configuration);
            var users = mapper.GetUsers();

            services.AddSingleton(users);
            services.AddSingleton<IUserStore, InMemoryUserStore>();
            services.AddTransient<IAuthorizationServerRuntimeClient, AuthorizationServerRuntimeClient>();
            services.AddAuthorizationPermissionPolicies();

            return services;
        }
    }
}