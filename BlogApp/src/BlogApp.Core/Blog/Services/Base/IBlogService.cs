namespace BlogApp.Core.Blog.Services.Base;

using BlogApp.Core.Blog.Models;

public interface IBlogService
{
    public Task CreateBlogAsync(Blog blog);
    public Task<Blog?> GetBlogByIdAsync(int id);
    public Task<IEnumerable<Blog?>> GetAllBlogsByUserId(int userId);
    public Task<IEnumerable<Blog?>> GetAllBlogsByTopicId(int topicId);
    public Task<IEnumerable<Blog?>> GetAllBlogsByName(string name);
}
