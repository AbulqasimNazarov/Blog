namespace BlogApp.Infrastructure.Blog.Queries;

using MediatR;
using BlogApp.Core.Blog.Models;

public class GetByIdQuery : IRequest<Blog?>
{
    public int? Id { get; set; }
}

