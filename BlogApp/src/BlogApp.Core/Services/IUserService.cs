using BlogApp.Core.Dtos;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services;

public interface IUserService
{
     public Task<User?> GetUserByIdAsync(int id); 
     public Task CreateAsync(RegistrationDto user);
     public Task<User?> IsSignedUpAsync(LoginDto user);
}


