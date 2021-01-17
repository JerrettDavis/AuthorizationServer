using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.Domain.Models
{
    public class Role : PersistentEntity
    {
        public Role(string name)
        {
            PermissionsAllowed = new HashSet<Permission>();
            PermissionsDenied = new HashSet<Permission>();

            Users = new HashSet<User>();

            Name = name;
        }

        public string Name { get; }

        public ICollection<Permission> PermissionsAllowed { get; set; }
        public ICollection<Permission> PermissionsDenied { get; set; }

        /// <summary>
        /// Permissions that exist in the allowed list, but not in the denied list.
        /// </summary>
        public virtual IEnumerable<Permission> EvaluatedPermissions =>
            PermissionsAllowed.Where(pa => !PermissionsDenied.Contains(pa));
        public virtual ICollection<User> Users { get; }

        public bool ContainsPermission(Permission permission)
        {
            return PermissionsAllowed.Contains(permission) ||
                   PermissionsDenied.Contains(permission);
        }

        public bool IsAllowed(Permission permission)
        {
            return PermissionsAllowed.Contains(permission) &&
                   !PermissionsDenied.Contains(permission);
        }

        protected bool Equals(Role other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Role) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}