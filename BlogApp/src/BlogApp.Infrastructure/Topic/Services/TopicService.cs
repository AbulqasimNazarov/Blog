namespace BlogApp.Infrastructure.Topic.Services;

using BlogApp.Core.Topic.Services.Base;
using BlogApp.Core.Topic.Repositories.Base;
using BlogApp.Core.Topic.Models;

public class TopicService : ITopicService
{
    private readonly ITopicRepository repository;

    public TopicService(ITopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Topic?>> GetAllTopicsAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Topic?> GetByIdAsync(int id)
    {
        if(id <= 0)
        {
            throw new ArgumentException("id is zero or below");
        }

        return await repository.GetByIdAsync(id);
    }
}
