using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface IRoleService
    {
        public Task<Role?> GetByNameAsync(string name);
        public Task CreateAsync(Role role);
        public Task<Role?> IGetByIdAsync(int id);
    }
}