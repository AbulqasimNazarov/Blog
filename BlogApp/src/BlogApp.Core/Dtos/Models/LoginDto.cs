using System.ComponentModel.DataAnnotations;

namespace BlogApp.Core.Dtos.Models;

public class LoginDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }

}
