using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Core.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Username { get; set; }
    public string? AvatarUrl { get; set; }
    [ForeignKey("RoleId"), Required]
    public int RoleId { get; set; }
    [Required]
    public string? Email { get; set; }
}
