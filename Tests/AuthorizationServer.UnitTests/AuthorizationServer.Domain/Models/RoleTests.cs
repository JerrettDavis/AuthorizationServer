using AuthorizationServer.Domain.Models;
using FluentAssertions;
using NUnit.Framework;

namespace AuthorizationServer.UnitTests.AuthorizationServer.Domain.Models
{
    public class RoleTests
    {
        [Test]
        public void RoleHasPermission_ShouldReturnTrue()
        {
            var permission = new Permission("Permission1");
            var role1 = new Role("Role1");

            role1.PermissionsAllowed.Add(permission);

            var result = role1.IsAllowed(permission);

            result.Should().BeTrue();
        }
        
        [Test]
        public void RoleDoesNotHavePermission_PermissionSetToAllowFalse_ShouldReturnFalse()
        {
            var permission = new Permission("Permission1");
            var role1 = new Role("Role1");

            role1.PermissionsDenied.Add(permission);

            var result = role1.IsAllowed(permission);

            result.Should().BeFalse();
        }
        
        [Test]
        public void RoleDoesNotHavePermission_PermissionDoesNotExist_ShouldReturnFalse()
        {
            var permission = new Permission("Permission1");
            var role1 = new Role("Role1");

            var result = role1.IsAllowed(permission);

            result.Should().BeFalse();
        }
        
        [Test]
        public void RoleContainsPermission_ShouldReturnTrue()
        {
            var permission = new Permission("Permission1");
            var role1 = new Role("Role1");

            role1.PermissionsAllowed.Add(permission);

            var result = role1.ContainsPermission(permission);

            result.Should().BeTrue();
        }
        
        [Test]
        public void RoleDoesNotContainPermission_ShouldReturnFalse()
        {
            var permission = new Permission("Permission1");
            var role1 = new Role("Role1");

            var result = role1.ContainsPermission(permission);

            result.Should().BeFalse();
        }
    }
}