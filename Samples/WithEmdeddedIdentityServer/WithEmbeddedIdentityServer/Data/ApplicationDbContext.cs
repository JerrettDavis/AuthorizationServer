using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WithEmbeddedIdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .HasData(new IdentityUser
                {
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "d4aa1684-1502-47ea-bf07-832f19ae05bc",
                    Email = "demo@authorizationserver.io",
                    EmailConfirmed = true,
                    Id = "0c303a4e-5c25-4d10-8396-c74c39c8610a",
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    NormalizedEmail = "DEMO@AUTHORIZATIONSERVER.IO",
                    NormalizedUserName = "DEMO@AUTHORIZATIONSERVER.IO",
                    PasswordHash = "AQAAAAEAACcQAAAAEJ4+zzSIOTx/Od09oUVXBzIsbKg1KqMJwTRWezU3AugKDZSJ64CPo3i5aK+5VnCVxA==",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = "S2SWX2DQRBDLJCUQNC4HF7H7IMZAUEAS",
                    TwoFactorEnabled = false,
                    UserName = "demo@authorizationserver.io"
                });
        }
    }
}