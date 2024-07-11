using System.ComponentModel.DataAnnotations;

namespace BlogApp.Core.Topic.Models;

public class Topic
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
