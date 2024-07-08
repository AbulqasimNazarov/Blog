using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;

        public RoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }
        public async Task CreateAsync(Role role)
        {
            if(role is null || role.Name is null)
            {
                throw new ArgumentNullException("role must have a name");
            }

            await repository.CreateAsync(role);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            if(name is null)
            {
                throw new ArgumentNullException("name must contain something");
            }

            return await repository.GetByNameAsync(name);
        }

        public async Task<Role?> IGetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}