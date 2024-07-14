namespace BlogApp.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.UserTopic.Repositories.Base;
using BlogApp.Infrastructure.UserTopic.Queries;

public class GetAllTopicsByUserIdHandler : IRequestHandler<GetAllTopicsByUserIdQuery, IEnumerable<Topic?>>
{
    private readonly IUserTopicRepository repository;
    public GetAllTopicsByUserIdHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Topic?>> Handle(GetAllTopicsByUserIdQuery request, CancellationToken cancellationToken)
    {
        if(request.UserId is null || request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        return await repository.GetAllTopicsByUserId(request.UserId.Value);
    }
}
