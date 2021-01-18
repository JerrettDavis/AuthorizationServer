using System.Threading.Tasks;
using AuthorizationServer.Models;

namespace AuthorizationServer.Common.Interfaces
{
    public interface IUserStore
    {
        Task<User?> FindUserBySubjectIdAsync(string subjectId);
    }
}