using System.Collections.Generic;
using AuthorizationServer.Models;

namespace WithEmbeddedIdentityServerAndConfigClasses
{
    public static class Config
    {
        public static IEnumerable<Role> GetRoles()
        {
            return new List<Role>
            {
                new()
                {
                    Name = "Admin",
                    PermissionsAllowed = new []{ "AccessAdmin" }
                } 
            };
        }

        public static IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new()
                {
                    SubjectId = "0c303a4e-5c25-4d10-8396-c74c39c8610a",
                    Roles = new[] {"Admin"}
                }
            };
        }
    }
}