namespace BlogApp.Infrastructure.Topic.Handlers;

using MediatR;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.Topic.Repositories.Base;
using BlogApp.Infrastructure.Topic.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, Topic?>
{
    private readonly ITopicRepository repository;
    public GetByIdHandler(ITopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Topic?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        if(request.Id is null || request.Id <= 0) {
            throw new ArgumentException("id is incorrect");
        }

        return await repository.GetByIdAsync(request.Id.Value);
    }
}