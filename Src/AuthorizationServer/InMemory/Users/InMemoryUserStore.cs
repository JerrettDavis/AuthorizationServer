using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Common.Interfaces;
using AuthorizationServer.Models;

namespace AuthorizationServer.InMemory.Users
{
    public class InMemoryUserStore : IUserStore
    {
        private readonly IEnumerable<User> _users;

        public InMemoryUserStore(IEnumerable<User> users)
        {
            _users = users;
        }
         
        public Task<User?> FindUserBySubjectIdAsync(string subjectId)
        {
            return Task.FromResult(_users.SingleOrDefault(u => u.SubjectId == subjectId));
        }
    }
}