namespace BlogApp.Core.UserTopic.Repositories.Base;

using BlogApp.Core.UserTopic.Models;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.Base.Methods;

public interface IUserTopicRepository : ICreateAsync<UserTopic>, IDeleteAsync<UserTopic>
{
    public Task<IEnumerable<Topic?>> GetAllTopicsByUserIdAsync(int userId);
    public Task CreateListAsync(IEnumerable<UserTopic?> userTopics);
}
