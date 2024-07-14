namespace BlogApp.Infrastructure.UserTopic.Commands;

using MediatR;

public class CreateCommand : IRequest
{
    public int TopicId { get; set; }
    public int UserId { get; set; }
}