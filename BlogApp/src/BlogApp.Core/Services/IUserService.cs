using BlogApp.Core.Models;

namespace BlogApp.Core.Services;

public interface IUserService
{
     public Task<User?> GetUserByIdAsync(int id); 
     public Task CreateAsync(User user);
}


