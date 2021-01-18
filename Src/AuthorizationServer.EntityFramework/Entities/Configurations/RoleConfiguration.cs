using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationServer.EntityFramework.Entities.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Name);
            builder.HasMany(e => e.PermissionsAllowed)
                .WithMany(e => e.RolesAllowed);
            builder.HasMany(e => e.PermissionsDenied)
                .WithMany(e => e.RolesDenied);
            builder.HasMany(e => e.Users)
                .WithMany(e => e.Roles);
        }
    }
}