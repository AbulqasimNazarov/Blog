using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories.Base;

namespace BlogApp.Core.Repositories
{
    public interface IUserTopicRepository : ICreateAsync<UserTopic>, IDeleteAsync<UserTopic>
    {
        public Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId);
    }
}