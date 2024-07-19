namespace BlogApp.Core.Blog.Repositories.Base;

using BlogApp.Core.Blog.Models;
using BlogApp.Core.Base.Methods;

public interface IBlogRepository: ICreateAsync<Blog>, IGetByIdAsync<Blog?>
{   
    public Task<IEnumerable<Blog?>> GetAllByUserIdAsync(int userId);
    public Task<IEnumerable<Blog?>> GetAllByTopicIdAsync(int topicId);
    public Task<IEnumerable<Blog?>> GetAllByNameAsync(string name);
    
}
