using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.Common.Models;
using AuthorizationServer.EntityFramework.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.EntityFramework.Evaluators
{
    public class UserEvaluator : IUserEvaluator
    {
        private readonly IAuthorizationDbContext _context;

        public UserEvaluator(IAuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task<UserSecurityDetails> GetUserSecurityDetailsAsync(
            string subjectId, 
            CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .ThenInclude(r => r.PermissionsAllowed)
                .Include(u => u.Roles)
                .ThenInclude(r => r.PermissionsDenied)
                .FirstOrDefaultAsync(u => 
                        u.SubjectId == subjectId, 
                    cancellationToken);

            if (user == null)
                return new UserSecurityDetails();

            return new UserSecurityDetails
            {
                Permissions = user.GetAllowedPermission().Select(p => p.Name),
                Roles = user.Roles.Select(r => r.Name)
            };
        }
    }
}