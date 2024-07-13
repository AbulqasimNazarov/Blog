namespace BlogApp.Infrastructure.Topic.Handlers;

using MediatR;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.Topic.Repositories.Base;
using BlogApp.Infrastructure.Topic.Queries;

public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<Topic?>>
{
    private readonly ITopicRepository repository;
    public GetAllHandler(ITopicRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Topic?>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync();
    }
}