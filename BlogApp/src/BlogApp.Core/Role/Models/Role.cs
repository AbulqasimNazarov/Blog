using System.ComponentModel.DataAnnotations;

namespace BlogApp.Core.Role.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
