using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class User
    {
        public string SubjectId { get; set; } = null!;
        public IEnumerable<string>? Roles { get; set; }
    }
}