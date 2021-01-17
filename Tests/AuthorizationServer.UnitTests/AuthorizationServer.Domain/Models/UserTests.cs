using AuthorizationServer.Domain.Models;
using FluentAssertions;
using NUnit.Framework;

namespace AuthorizationServer.UnitTests.AuthorizationServer.Domain.Models
{
    public class UserTests
    {
        [Test]
        public void UserInRole_ShouldReturnTrue()
        {
            var role1 = new Role("Role1");
            var role2 = new Role("Role2");
            var user = new User("User1");

            role1.Users.Add(user);
            role2.Users.Add(user);
            user.Roles.Add(role1);
            user.Roles.Add(role2);

            var result = user.InRole(role1);

            result.Should().BeTrue();
        }

        [Test]
        public void UserNotInRole_ShouldReturnFalse()
        {
            var role1 = new Role("Role1");
            var role2 = new Role("Role2");
            var user = new User("User1");

            role1.Users.Add(user);
            user.Roles.Add(role1);

            var result = user.InRole(role2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void UserHasPermissions_ShouldReturnTrue()
        {
            var permission1 = new Permission("Permission1");
            var permission2 = new Permission("Permission2");
            var role1 = new Role("Role1");
            var user = new User("User1");
            
            role1.PermissionsAllowed.Add(permission1);
            role1.PermissionsDenied.Add(permission2);
            
            role1.Users.Add(user);
            user.Roles.Add(role1);

            var result = user.HasPermission(permission1);

            result.Should().BeTrue();
        }
        
        [Test]
        public void UserDoesNotHavePermissions_ShouldReturnFalse()
        {
            var permission1 = new Permission("Permission1");
            var permission2 = new Permission("Permission2");
            var role1 = new Role("Role1");
            var user = new User("User1");
            
            role1.PermissionsAllowed.Add(permission1);
            role1.PermissionsDenied.Add(permission2);
            
            role1.Users.Add(user);
            user.Roles.Add(role1);

            var result = user.HasPermission(permission2);

            result.Should().BeFalse();
        }
        
                
        [Test]
        public void UserDoesNotHavePermission_OtherRoleOverrides_ShouldReturnFalse()
        {
            var permission1 = new Permission("Permission1");
            var permission2 = new Permission("Permission2");
            var role1 = new Role("Role1");
            var role2 = new Role("Role2");
            var user = new User("User1");
            
            role1.PermissionsAllowed.Add(permission1);
            role1.PermissionsDenied.Add(permission2);
            role2.PermissionsDenied.Add(permission1);
            role2.PermissionsDenied.Add(permission2);
            
            role1.Users.Add(user);
            role2.Users.Add(user);
            user.Roles.Add(role1);
            user.Roles.Add(role2);

            var result = user.HasPermission(permission2);

            result.Should().BeFalse();
        }
        
        [Test]
        public void GetAllowedPermissions_ShouldReturnAllAllowedPermissions()
        {
            var permission1 = new Permission("Permission1");
            var permission2 = new Permission("Permission2");

            var role1 = new Role("Role1");
            var role2 = new Role("Role2");
            
            var user = new User("User");
            
            role1.PermissionsAllowed.Add(permission1);
            role1.PermissionsAllowed.Add(permission2);
            role2.PermissionsDenied.Add(permission1);
            role2.PermissionsAllowed.Add(permission2);
            
            role1.Users.Add(user);
            role2.Users.Add(user);
            
            user.Roles.Add(role1);
            user.Roles.Add(role2);

            var result = user.GetAllowedPermission();
            
            result.Should().HaveCount(1);
        }
    }
}