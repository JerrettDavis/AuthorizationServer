using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Common.Models;

namespace AuthorizationServer.Common.Interfaces
{
    public interface IUserEvaluator
    {
        Task<IEnumerable<string>> GetPermissions(
            string subjectId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<string>> GetRoles(
            string subjectId,
            CancellationToken cancellationToken = default);

        Task<UserSecurityDetails> GetUserSecurityDetailsAsync(
            string subjectId,
            CancellationToken cancellationToken = default);
    }
}