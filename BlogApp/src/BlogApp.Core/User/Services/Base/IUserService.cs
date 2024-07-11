namespace BlogApp.Core.User.Services.Base;

using BlogApp.Core.Dtos.Models;
using BlogApp.Core.User.Models;

public interface IUserService
{
     public Task<User?> GetUserByIdAsync(int id); 
     public Task<User?> GetUserByEmailAsync(string email); 
     public Task CreateAsync(RegistrationDto user);
     public Task<User?> GetSignedUpUser(LoginDto user);
}


