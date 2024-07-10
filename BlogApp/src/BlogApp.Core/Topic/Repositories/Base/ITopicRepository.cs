namespace BlogApp.Core.Topic.Repositories.Base;

using BlogApp.Core.Topic.Models;
using BlogApp.Core.Base.Methods;

public interface ITopicRepository : IGetByIdAsync<Topic?>, IGetAllAsync<Topic?>
{
    
}
