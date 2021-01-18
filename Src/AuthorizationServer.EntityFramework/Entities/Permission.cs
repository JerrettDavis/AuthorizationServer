using System.Collections.Generic;

namespace AuthorizationServer.EntityFramework.Entities
{
    public partial class Permission
    {
        public Permission(string name)
        {
            RolesAllowed = new HashSet<Role>();
            RolesDenied = new HashSet<Role>();

            Name = name;
        }

        public string Name { get; }

        public ICollection<Role> RolesAllowed { get; }
        public ICollection<Role> RolesDenied { get; }

        protected bool Equals(Permission other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Permission) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}