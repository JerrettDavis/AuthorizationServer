using System.Collections.Generic;
using System.Linq;
using AuthorizationServer.Application.Common;
using AuthorizationServer.Application.Configurations.DependencyInjection.Options;
using AuthorizationServer.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace AuthorizationServer.Application.InMemory.Configuration
{
    public class ConfigurationMapper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<User> GetUsers()
        {
            var userOptions = new List<UserOptions>();
            var roleOptions = new List<RoleOptions>();
            var permissionOptions = new List<string>();
            
            _configuration.GetSection(ApplicationConstants.UserOptions).Bind(userOptions);
            _configuration.GetSection(ApplicationConstants.RoleOptions).Bind(roleOptions);
            _configuration.GetSection(ApplicationConstants.PermissionOptions).Bind(permissionOptions);

            var permissions = permissionOptions.Select(p => new Permission(p))
                .ToDictionary(p => p.Name, p => p);
            var roles = roleOptions.Select(r => new Role(r.Name)
            {
                PermissionsAllowed = r.PermissionsAllowed?
                    .Select(pa => permissions[pa])
                    .ToList() ?? 
                                     new List<Permission>(),
                PermissionsDenied = r.PermissionsDenied?
                    .Select(pd => permissions[pd])
                    .ToList() ??
                                    new List<Permission>()
            }).ToDictionary(r => r.Name, r => r);
            return userOptions.Select(u => new User(u.SubjectId)
            {
                Roles = u.Roles?.Select(r => roles[r]).ToList() ??
                        new List<Role>()
            });
        }
    }
}