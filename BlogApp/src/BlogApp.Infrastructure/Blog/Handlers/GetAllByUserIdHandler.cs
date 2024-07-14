namespace BlogApp.Infrastructure.Blog.Handlers;

using MediatR;
using BlogApp.Core.Blog.Models;
using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Infrastructure.Blog.Queries;

public class GetAllByUserIdHandler : IRequestHandler<GetAllByUserIdQuery, IEnumerable<Blog?>>
{
    private readonly IBlogRepository repository;
    public GetAllByUserIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Blog?>> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
    {
        if(request.UserId is null || request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        return await repository.GetAllByUserId(request.UserId.Value);
    }
}
