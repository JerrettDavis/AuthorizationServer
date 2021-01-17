using System.Threading.Tasks;
using AuthorizationServer.Domain.Models;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface IUserStore
    {
        Task<User?> FindUserBySubjectIdAsync(string subjectId);
    }
}