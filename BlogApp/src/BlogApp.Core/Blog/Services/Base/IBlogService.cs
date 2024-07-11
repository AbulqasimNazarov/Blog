namespace BlogApp.Core.Blog.Services.Base;

using BlogApp.Core.Blog.Models;
using Microsoft.AspNetCore.Http;

public interface IBlogService
{
    public Task CreateBlogAsync(Blog blog, IFormFile image);
    public Task<Blog?> GetBlogByIdAsync(int id);
    public Task<IEnumerable<Blog?>> GetAllBlogsByUserId(int userId);
    public Task<IEnumerable<Blog?>> GetAllBlogsByTopicId(List<int> topicId);
    public Task<IEnumerable<Blog?>> GetAllBlogsByName(string name);
}
