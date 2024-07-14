namespace BlogApp.Infrastructure.Blog.Queries;

using MediatR;
using BlogApp.Core.Blog.Models;

public class GetAllByTopicIdQuery : IRequest<IEnumerable<Blog?>>
{
    public int? TopicId { get; set; }
}
