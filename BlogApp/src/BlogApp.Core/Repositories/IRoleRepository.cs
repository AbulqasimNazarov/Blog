using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories.Base;

namespace BlogApp.Core.Repositories
{
    public interface IRoleRepository : ICreateAsync<Role>, IGetByIdAsync<Role?>
    {
        public Task<Role?> GetByNameAsync(string name);
    }
}