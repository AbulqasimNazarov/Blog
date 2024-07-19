namespace BlogApp.Infrastructure.UserTopic.Commands;

using MediatR;
using BlogApp.Core.Topic.Models;

public class CreateListCommand : IRequest
{
    public IEnumerable<Topic?>? Topics { get; set; }
    public int UserId { get; set; }
}