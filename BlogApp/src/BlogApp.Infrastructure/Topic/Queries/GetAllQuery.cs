namespace BlogApp.Infrastructure.Topic.Queries;

using MediatR;
using BlogApp.Core.Topic.Models;

public class GetAllQuery : IRequest<IEnumerable<Topic?>>
{
    
}