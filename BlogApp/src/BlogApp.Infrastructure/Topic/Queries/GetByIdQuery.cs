namespace BlogApp.Infrastructure.Topic.Queries;

using MediatR;
using BlogApp.Core.Topic.Models;

public class GetByIdQuery : IRequest<Topic?>
{
    public int? Id { get; set; }
}