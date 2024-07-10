namespace BlogApp.Core.Topic.Services.Base;

using BlogApp.Core.Topic.Models;

public interface ITopicService
{
    public Task<Topic?> GetByIdAsync(int id);
    public Task<IEnumerable<Topic?>> GetAllTopicsAsync();
}
