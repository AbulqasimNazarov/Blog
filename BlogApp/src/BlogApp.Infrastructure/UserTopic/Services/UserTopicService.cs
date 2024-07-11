namespace BlogApp.Infrastructure.UserTopic.Services;

using BlogApp.Core.UserTopic.Services.Base;
using BlogApp.Core.UserTopic.Repositories.Base;
using BlogApp.Core.UserTopic.Models;
using BlogApp.Core.Topic.Models;

public class UserTopicService : IUserTopicService
{
    private readonly IUserTopicRepository repository;

    public UserTopicService(IUserTopicRepository repository)
    {
        this.repository = repository;
    }
    public async Task AddTopicToInterestAsync(UserTopic userTopic)
    {
        if(userTopic ==  null)
        {
            throw new ArgumentNullException("userTopic is null");
        }

        await repository.CreateAsync(userTopic, null);
    }

    public async Task DeleteTopicFromInterestAsync(UserTopic userTopic)
    {
        if(userTopic ==  null)
        {
            throw new ArgumentNullException("userTopic is null");
        }

        await repository.DeleteAsync(userTopic);
    }

    public async Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId)
    {
        if(userId <= 0)
        {
            throw new ArgumentException("userId cannot be zero or below");
        }
        
        return await repository.GetAllTopicsByUserId(userId);
    }
}
