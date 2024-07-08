using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository repository;

        public BlogService(IBlogRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateBlogAsync(Blog blog)
        {
            if(blog == null || blog.Text == null || blog.Title == null || blog.TopicId == null || blog.UserId == null)
            {
                throw new ArgumentNullException("blog cannot be created, some prop(s) is(are) null");
            }

            await repository.CreateAsync(blog);
        }

        public async Task<IEnumerable<Blog?>> GetAllBlogsByName(string name)
        {
            if(name is null)
            {
                throw new ArgumentNullException("param is null");
            }

            return await repository.GetAllByName(name);
        }

        public async Task<IEnumerable<Blog?>> GetAllBlogsByTopicId(int topicId)
        {
            return await repository.GetAllByTopicId(topicId);
        }

        public async Task<IEnumerable<Blog?>> GetAllBlogsByUserId(int userId)
        {
            return await repository.GetAllByUserId(userId);
        }

        public async Task<Blog?> GetBlogByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}