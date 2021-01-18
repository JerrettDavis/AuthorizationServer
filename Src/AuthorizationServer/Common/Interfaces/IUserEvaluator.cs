using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Common.Models;

namespace AuthorizationServer.Common.Interfaces
{
    public interface IUserEvaluator
    {
        Task<UserSecurityDetails> GetUserSecurityDetailsAsync(
            string subjectId,
            CancellationToken cancellationToken = default);
    }
}