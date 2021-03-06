﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuthorizationServer.Models;

namespace AuthorizationServer.Common.Interfaces
{
    public interface IRoleStore
    {
        Task<IEnumerable<Role>> GetRolesByNameAsync(
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default);
    }
}