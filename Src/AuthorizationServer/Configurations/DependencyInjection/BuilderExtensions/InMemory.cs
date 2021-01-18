using System.Collections.Generic;
using AuthorizationServer.Common;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.InMemory.Roles;
using AuthorizationServer.InMemory.Users;
using AuthorizationServer.Models;
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
            services
                .AddAuthorizationServer()
                .AddInMemoryRoles(configuration)
                .AddInMemoryRoleStore()
                .AddInMemoryUsers(configuration)
                .AddInMemoryUserStore()
                .AddInMemoryUserEvaluator();

            return services;
        }

        public static IServiceCollection AddInMemoryRoleStore(
            this IServiceCollection services)
        {
            services.AddSingleton<IRoleStore, InMemoryRoleStore>();
            
            return services;
        }

        public static IServiceCollection AddInMemoryRoles(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var roles = configuration
                .GetSection(ApplicationConstants.RoleOptions)
                .Get<List<Role>>();

            return services.AddInMemoryRoles(roles);
        }

        public static IServiceCollection AddInMemoryRoles(
            this IServiceCollection services,
            IEnumerable<Role> roles)
        {
            services.AddSingleton(roles);

            return services;
        }

        public static IServiceCollection AddInMemoryUserEvaluator(
            this IServiceCollection services)
        {
            services.AddSingleton<IUserEvaluator, InMemoryUserEvaluator>();

            return services;
        }

        public static IServiceCollection AddInMemoryUserStore(
            this IServiceCollection services)
        {
            services.AddSingleton<IUserStore, InMemoryUserStore>();
            
            return services;
        }

        public static IServiceCollection AddInMemoryUsers(
            this IServiceCollection services,
            IEnumerable<User> users)
        {
            services.AddSingleton(users);

            return services;
        }

        public static IServiceCollection AddInMemoryUsers(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var users = configuration
                .GetSection(ApplicationConstants.UserOptions)
                .Get<List<User>>();

            return services.AddInMemoryUsers(users);
        }
    }
}