using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface IUserRoleService
    {
        public Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId);
        public Task ChangeAsync(int userId, Role role);
        public Task CreateAsync(UserRole userRole);
        public Task DeleteAsync(UserRole userRole);
    }
}