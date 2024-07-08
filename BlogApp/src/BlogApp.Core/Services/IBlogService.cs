using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface IBlogService
    {
        public Task CreateBlogAsync(Blog blog);
        public Task<Blog?> GetBlogByIdAsync(int id);
        public Task<IEnumerable<Blog?>> GetAllBlogsByUserId(int userId);
        public Task<IEnumerable<Blog?>> GetAllBlogsByTopicId(int topicId);
        public Task<IEnumerable<Blog?>> GetAllBlogsByName(string name);
    }
}