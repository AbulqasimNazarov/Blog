namespace BlogApp.Infrastructure.Blog.Handlers;

using MediatR;
using BlogApp.Core.Blog.Models;
using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Infrastructure.Blog.Queries;

public class GetAllByTopicIdHandler : IRequestHandler<GetAllByTopicIdQuery, IEnumerable<Blog?>>
{
    private readonly IBlogRepository repository;
    public GetAllByTopicIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Blog?>> Handle(GetAllByTopicIdQuery request, CancellationToken cancellationToken)
    {
        if(request.TopicId is null || request.TopicId <= 0) {
            throw new ArgumentException("topicId is incorrect");
        }

        return await repository.GetAllByTopicIdAsync(request.TopicId.Value);
    }
}
