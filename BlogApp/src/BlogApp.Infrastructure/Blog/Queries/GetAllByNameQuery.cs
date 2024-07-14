namespace BlogApp.Infrastructure.Blog.Queries;

using BlogApp.Core.Blog.Models;
using MediatR;

public class GetAllByNameQuery : IRequest<IEnumerable<Blog?>>
{
    public string? Name { get; set; }
}
