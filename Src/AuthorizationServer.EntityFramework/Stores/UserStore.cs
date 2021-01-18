using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.EntityFramework.Common.Interfaces;
using AuthorizationServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.EntityFramework.Stores
{
    public class UserStore : IUserStore
    {
        private readonly IAuthorizationDbContext _context;

        public UserStore(IAuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> FindUserBySubjectIdAsync(string subjectId)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Select(u => new User
                {
                    SubjectId = u.SubjectId,
                    Roles = u.Roles.Select(r => r.Name)
                })
                .FirstOrDefaultAsync(u => u.SubjectId == subjectId);
        }
    }
}