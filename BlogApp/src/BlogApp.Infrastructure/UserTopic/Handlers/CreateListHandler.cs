#pragma warning disable CS8602

namespace BlogApp.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogApp.Infrastructure.UserTopic.Commands;
using System.Threading.Tasks;
using System.Threading;
using BlogApp.Core.UserTopic.Repositories.Base;
using BlogApp.Core.UserTopic.Models;

public class CreateListHandler : IRequestHandler<CreateListCommand>
{
    private readonly IUserTopicRepository repository;
    public CreateListHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }
    public async Task Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        
        if(request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        var areValid = request?.Topics?.Any(topic => topic is null || topic.Id <= 0) ?? false;

        if(areValid)
        {
            throw new ArgumentException("topics are incorrect");
        }

        var userTopics = request?.Topics?.Select(topic => new UserTopic()
        {
            UserId = request.UserId,
            TopicId = topic.Id
        });

        await repository.CreateListAsync(userTopics!);
    }
}
