namespace BlogApp.Infrastructure.Blog.Queries;

using MediatR;
using BlogApp.Core.Blog.Models;

public class GetAllByUserIdQuery : IRequest<IEnumerable<Blog?>>
{
    public int? UserId { get; set; }
}