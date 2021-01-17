using System.Collections.Generic;

namespace AuthorizationServer.Application.Configurations.DependencyInjection.Options
{
    public class UserOptions
    {
        public string SubjectId { get; set; } = null!;
        public IEnumerable<string>? Roles { get; set; }
    }
}