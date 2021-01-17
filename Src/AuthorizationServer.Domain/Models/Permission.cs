using System.Collections.Generic;

namespace AuthorizationServer.Domain.Models
{
    public class Permission : PersistentEntity
    {
        public Permission(string name)
        {
            Roles = new HashSet<Role>();
            
            Name = name;
        }

        public string Name { get; }
        
        public ICollection<Role> Roles { get; private set; }

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