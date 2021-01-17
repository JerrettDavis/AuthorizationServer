using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Application.Common.Interfaces;
using AuthorizationServer.Domain.Models;

namespace AuthorizationServer.Application.InMemory.Users
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