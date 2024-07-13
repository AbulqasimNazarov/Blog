namespace BlogApp.Infrastructure.UserTopic.Queries;

using MediatR;
using BlogApp.Core.Topic.Models;

public class GetAllTopicsByUserIdQuery : IRequest<IEnumerable<Topic?>>
{
    public int? UserId { get; set; }
}