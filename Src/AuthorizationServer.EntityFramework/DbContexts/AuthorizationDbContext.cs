using AuthorizationServer.EntityFramework.Common.Interfaces;
using AuthorizationServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.EntityFramework.DbContexts
{
    public class AuthorizationDbContext : AuthorizationDbContext<AuthorizationDbContext>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
            : base(options)
        {
        }
    }

    public class AuthorizationDbContext<TContext> : DbContext, IAuthorizationDbContext
        where TContext : DbContext, IAuthorizationDbContext
    {
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        
        public AuthorizationDbContext(DbContextOptions<TContext> options)
            : base(options)
        {
        }
    }
}