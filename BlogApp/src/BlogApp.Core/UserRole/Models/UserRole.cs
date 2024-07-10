using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Core.UserRole.Models;

using BlogApp.Core.User.Models;
using BlogApp.Core.Role.Models;

public class UserRole
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("RoleId"), Required]
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    [ForeignKey("UserId"), Required]
    public int UserId { get; set; }
    public User? User { get; set; }
}
