using System;
using AuthorizationServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.EntityFramework.Common.Interfaces
{
    public interface IAuthorizationDbContext : IDisposable
    {
        DbSet<Permission> Permissions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
    }
}