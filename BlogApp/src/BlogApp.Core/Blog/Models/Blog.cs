using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Core.Blog.Models;

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
    [ForeignKey("UserId"), Required]
    public int? UserId { get; set; }
    public string? PictureUrl { get; set; } 
}
