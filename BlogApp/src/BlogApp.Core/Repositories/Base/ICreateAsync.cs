using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Core.Repositories.Base
{
    public interface ICreateAsync<TEntity>
    {
        public Task CreateAsync(TEntity entity);
    }
}