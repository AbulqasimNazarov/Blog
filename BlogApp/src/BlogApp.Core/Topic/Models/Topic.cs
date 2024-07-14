using System.ComponentModel.DataAnnotations;

namespace BlogApp.Core.Topic.Models;

using BlogApp.Core.UserTopic.Models;

public class Topic
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public IEnumerable<UserTopic>? Users {set; get;}
}
