using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Core.Blog.Models;

using BlogApp.Core.User.Models;
using BlogApp.Core.Topic.Models;

public class Blog
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Text { get; set; }
    [ForeignKey("TopicId"), Required]
    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }
    [ForeignKey("UserId"), Required]
    public int? UserId { get; set; }
    public User? User { get; set; }
    public string? PictureUrl { get; set; } 
    [Required]
    public DateTime? CreationDate{set; get;}
}
