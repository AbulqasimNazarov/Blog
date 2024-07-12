using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.User.Models;

public class User : IdentityUser
{
    [Required]
    public string? Name { get; set; }
    public string? AvatarUrl { get; set; }
}
