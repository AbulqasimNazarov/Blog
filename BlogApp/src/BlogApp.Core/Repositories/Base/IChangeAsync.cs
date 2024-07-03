using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Core.Repositories.Base
{
    public interface IChangeAsync<TEntity>
    {
        public Task ChangeAsync(int id, TEntity entity);
    }
}