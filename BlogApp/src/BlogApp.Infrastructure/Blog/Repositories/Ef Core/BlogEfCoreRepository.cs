#pragma warning disable CS1998
#pragma warning disable CS8602

namespace BlogApp.Infrastructure.Blog.Repositories.Ef_Core;

using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Core.Blog.Models;
using BlogApp.Core.Blog.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using BlogApp.Infrastructure.Data.DbContext;

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
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Blog?>> GetAllByNameAsync(string name)
    {
        return dbContext.Blogs.Where(blog => blog.Title.StartsWith(name));
    }

    public async Task<IEnumerable<Blog?>> GetAllByTopicIdAsync(int topicId)
    {
        var blogs = await dbContext.Blogs
        .Where(blog => blog.TopicId == topicId)
        .Include(blog => blog.User)
        .Include(blog => blog.Topic)
        .ToListAsync();

        return blogs.AsEnumerable();
    }

    public async Task<IEnumerable<Blog?>> GetAllByUserIdAsync(int userId)
    {
        return dbContext.Blogs.Where(blog => blog.UserId == userId)
            .Include(blog => blog.User)
            .Include(blog => blog.Topic);
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await dbContext.Blogs
            .Include(blog => blog.User)    // загрузка данных о пользователе
            .Include(blog => blog.Topic)   // загрузка данных о теме
            .FirstOrDefaultAsync(blog => blog.Id == id);
    }
}
