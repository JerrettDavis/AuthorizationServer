using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.EntityFramework.Entities
{
    public partial class User
    {
        private User(Guid id, string subjectId)
        {
            Roles = new HashSet<Role>();

            Id = id;
            SubjectId = subjectId;
        }

        public User(string subjectId) : this(Guid.NewGuid(), subjectId)
        {
        }

        public Guid Id { get; }
        public string SubjectId { get; }

        public ICollection<Role> Roles { get; set; }

        public bool InRole(Role role)
        {
            return Roles.Any(r => Equals(r, role));
        }

        public bool HasPermission(Permission permission)
        {
            return Roles.Where(r => r.ContainsPermission(permission))
                .All(r => r.IsAllowed(permission));
        }

        public IEnumerable<Permission> GetAllowedPermission()
        {
            var denied = Roles.SelectMany(r => r.PermissionsDenied);
            return Roles.SelectMany(r => r.EvaluatedPermissions)
                .Where(r => !denied.Contains(r))
                .GroupBy(r => r.Name)
                .Select(r => r.First());
        }
    }
}