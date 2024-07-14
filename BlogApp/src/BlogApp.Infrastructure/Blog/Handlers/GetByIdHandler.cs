namespace BlogApp.Infrastructure.Blog.Handlers;

using MediatR;
using BlogApp.Core.Blog.Models;
using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Infrastructure.Blog.Queries;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, Blog?>
{
    private readonly IBlogRepository repository;
    public GetByIdHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Blog?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        if(request.Id is null || request.Id <= 0) {
            throw new ArgumentException("id is incorrect");
        }

        return await repository.GetByIdAsync(request.Id.Value);
    }
}