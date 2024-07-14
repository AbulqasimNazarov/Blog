namespace BlogApp.Infrastructure.UserTopic.Handlers;

using MediatR;
using BlogApp.Infrastructure.UserTopic.Commands;
using System.Threading.Tasks;
using System.Threading;
using BlogApp.Core.UserTopic.Repositories.Base;
using BlogApp.Core.UserTopic.Models;

public class DeleteHandler : IRequestHandler<DeleteCommand>
{
    private readonly IUserTopicRepository repository;
    public DeleteHandler(IUserTopicRepository repository)
    {
        this.repository = repository;
    }
    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        if(request.Id <= 0) {
            throw new ArgumentException("id is incorrect");
        }

        var userTopic = new UserTopic()
        {
            Id = request.Id,
        };

        await repository.DeleteAsync(userTopic);
    }
}
