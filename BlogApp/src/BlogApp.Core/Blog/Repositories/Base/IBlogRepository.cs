namespace BlogApp.Core.Blog.Repositories.Base;

using BlogApp.Core.Blog.Models;
using BlogApp.Core.Base.Methods;

public interface IBlogRepository: ICreateAsync<Blog>, IGetByIdAsync<Blog?>
{   
    public Task<IEnumerable<Blog?>> GetAllByUserId(int userId);
    public Task<IEnumerable<Blog?>> GetAllByTopicId(int topicId);
    public Task<IEnumerable<Blog?>> GetAllByName(string name);
    
}
