using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationServer.EntityFramework.Entities.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(e => e.Name);
            builder.HasMany(e => e.RolesAllowed)
                .WithMany(e => e.PermissionsAllowed);
            builder.HasMany(e => e.RolesDenied)
                .WithMany(e => e.PermissionsDenied);
        }
    }
}