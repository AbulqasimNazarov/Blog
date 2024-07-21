using MediatR;

namespace BlogApp.Infrastructure.Blog.Commands;

public class CreateCommand : IRequest
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    public int? TopicId { get; set; }
    public int? UserId { get; set; }
    public DateTime? CreationDate{set; get;}
    public string? PictureUrl { get; set; }
}
