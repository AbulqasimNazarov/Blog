namespace BlogApp.Infrastructure.UserTopic.Commands;

using MediatR;

public class DeleteCommand : IRequest
{
    public int Id { get; set; }
}