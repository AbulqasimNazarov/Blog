namespace BlogApp.Core.UserTopic.Services.Base;

using BlogApp.Core.UserTopic.Models;
using BlogApp.Core.Topic.Models;

public interface IUserTopicService
{
    public Task AddTopicToInterestAsync(UserTopic userTopic); // represents deleting topic to user interest
    public Task DeleteTopicFromInterestAsync(UserTopic userTopic); // represents adding topic to user interest
    public Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId);
}
