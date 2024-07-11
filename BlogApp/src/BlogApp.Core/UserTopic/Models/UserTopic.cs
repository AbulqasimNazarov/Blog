using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Core.UserTopic.Models;

using BlogApp.Core.User.Models;
using BlogApp.Core.Topic.Models;

public class UserTopic
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("TopicId"), Required]
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    [ForeignKey("UserId"), Required]
    public int UserId { get; set; }
    public User? User { get; set; }
}
