using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories.Base;

namespace BlogApp.Core.Repositories
{
    public interface IBlogRepository: ICreateAsync<Blog>, IGetByIdAsync<Blog?>
    {   
        public Task<IEnumerable<Blog?>> GetAllByUserId(int userId);
        public Task<IEnumerable<Blog?>> GetAllByTopicId(int topicId);
        public Task<IEnumerable<Blog?>> GetAllByName(string name);
        
    }
}