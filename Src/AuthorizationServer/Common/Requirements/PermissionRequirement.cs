using Microsoft.AspNetCore.Authorization;

// ReSharper disable once CheckNamespace
namespace AuthorizationServer.AspNetCore.Common.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
    }
}