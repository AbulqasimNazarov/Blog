using BlogApp.Core.Models;

namespace BlogApp.Core.Services;

public interface IUserService
{
     public User GetUserById(Guid id);   
}


