using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.Common.Models;
using AuthorizationServer.Models;


namespace AuthorizationServer.InMemory.Users
{
    public class InMemoryUserEvaluator : IUserEvaluator
    {
        private readonly IUserStore _userStore;
        private readonly IRoleStore _roleStore;

        public InMemoryUserEvaluator(
            IUserStore userStore, 
            IRoleStore roleStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
        }

        public async Task<IEnumerable<string>> GetPermissions(
            string subjectId, 
            CancellationToken cancellationToken = default)
        {
            var user = await _userStore.FindUserBySubjectIdAsync(subjectId);
            if (user == null || user.Roles?.Any() != null)
                return new List<string>();

            return await GetPermissions(user);
        }

        private async Task<IEnumerable<string>> GetPermissions(
            User user)
        {
            var roles = (await _roleStore
                    .GetRolesByNameAsync(user.Roles ?? 
                                         new List<string>()))
                .ToList();

            var allAllowed = roles
                .SelectMany(r => r.PermissionsAllowed ?? new List<string>())
                .Distinct();
            var allDenied = roles
                .SelectMany(r => r.PermissionsDenied ?? new List<string>())
                .Distinct();

            return allAllowed.Where(aa => !allDenied.Contains(aa));
        } 

        public async Task<IEnumerable<string>> GetRoles(
            string subjectId, 
            CancellationToken cancellationToken = default)
        {
            var user = await _userStore.FindUserBySubjectIdAsync(subjectId);
            if (user == null || user.Roles?.Any() != true)
                return new List<string>();
            return user.Roles;
        }

        public async Task<UserSecurityDetails> GetUserSecurityDetailsAsync(
            string subjectId, 
            CancellationToken cancellationToken = default)
        {
            var user = await _userStore.FindUserBySubjectIdAsync(subjectId);
            if (user == null || user.Roles?.Any() != true)
                return new UserSecurityDetails(
                    new List<string>(), 
                    new List<string>());

            var permissions = await GetPermissions(user);
            var roles = user.Roles ?? new List<string>();

            return new UserSecurityDetails(roles, permissions);
        }
    }
}