using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Core.User.Models;

using BlogApp.Core.UserTopic.Models;

public class User : IdentityUser<int>
{
    [Required]
    public string? Name { get; set; }
    public string? AvatarUrl { get; set; }
    public IEnumerable<UserTopic>? Topics {set; get;}
}
