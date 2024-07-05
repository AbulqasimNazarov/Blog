using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository repository;

        public UserRoleService(IUserRoleRepository repository)
        {
            this.repository = repository;
        }
        public async Task ChangeAsync(int userId, Role role)
        {
            if(role is null || role.Name is null)
            {
                throw new ArgumentException("role must have a name");
            }

            await repository.ChangeAsync(userId, role);
        }

        public async Task CreateAsync(UserRole userRole)
        {
            if(userRole is null)
            {
                throw new ArgumentException("userRole must contain something");
            }

            await repository.CreateAsync(userRole);
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            if(userRole is null)
            {
                throw new ArgumentException("userRole must contain something");
            }

            await repository.DeleteAsync(userRole);
        }

        public async Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId)
        {
            return await repository.GetAllRolesByUserIdAsync(userId);
        }
    }
}