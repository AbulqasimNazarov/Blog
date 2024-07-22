namespace BlogApp.Infrastructure.Blog.Handlers;

using MediatR;
using BlogApp.Infrastructure.Blog.Commands;
using System.Threading.Tasks;
using System.Threading;
using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Core.Blog.Models;

public class CreateHandler : IRequestHandler<CreateCommand>
{
    private readonly IBlogRepository repository;
    public CreateHandler(IBlogRepository repository)
    {
        this.repository = repository;
    }
    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.Title) || string.IsNullOrWhiteSpace(request.Title)) {
            throw new ArgumentException("title is empty");
        }
        else if(string.IsNullOrEmpty(request.Text) || string.IsNullOrWhiteSpace(request.Text)) {
            throw new ArgumentException("text is empty");
        }
        else if(request.TopicId is null || request.TopicId <= 0) {
            throw new ArgumentException("topicId is incorrect");
        }
        else if(request.UserId is null || request.UserId <= 0) {
            throw new ArgumentException("userId is incorrect");
        }

        var blog = new Blog {
            Title = request.Title,
            Text = request.Text,
            TopicId = request.TopicId.Value,
            UserId = request.UserId.Value,
            PictureUrl = request.PictureUrl,
            CreationDate = DateTime.UtcNow
        };

        await repository.CreateAsync(blog);
    }
}
