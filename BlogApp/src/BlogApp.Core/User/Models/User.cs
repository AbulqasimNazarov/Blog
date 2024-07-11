using System.ComponentModel.DataAnnotations;

namespace BlogApp.Core.User.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? AvatarUrl { get; set; }
    [Required]
    public string? Email { get; set; }
}
