#pragma warning disable CS1998
#pragma warning disable CS8602

namespace BlogApp.Infrastructure.Blog.Repositories.Ef_Core;

using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Core.Blog.Models;
using BlogApp.Core.Blog.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MyGames.Infrastructure.Data.DbContext;

public class BlogEfCoreRepository : IBlogRepository
{
    private readonly BlogDbContext dbContext;
    public BlogEfCoreRepository(BlogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(Blog blog)
    {
        await dbContext.Blogs.AddAsync(blog);
    }

    public async Task<IEnumerable<Blog?>> GetAllByName(string name)
    {
        return dbContext.Blogs.Where(blog => blog.Title.StartsWith(name));
    }

    public async Task<IEnumerable<Blog?>> GetAllByTopicId(int topicId)
    {
        return dbContext.Blogs.Where(blog => blog.TopicId == topicId);
    }

    public async Task<IEnumerable<Blog?>> GetAllByUserId(int userId)
    {
        return dbContext.Blogs.Where(blog => blog.TopicId == userId);
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await dbContext.Blogs.Where(blog => blog.Id == id).FirstOrDefaultAsync();
    }
}
