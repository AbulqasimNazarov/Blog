using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories.Base;

namespace BlogApp.Core.Repositories
{
    public interface IUserRoleRepository : ICreateAsync<UserRole>, IDeleteAsync<UserRole>
    {
        public Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId);
        public Task ChangeAsync(int userId, Role role);
    }
}