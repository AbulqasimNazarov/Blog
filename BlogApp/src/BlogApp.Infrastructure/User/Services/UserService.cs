namespace BlogApp.Infrastructure.User.Services;

using BlogApp.Core.User.Services.Base;
using BlogApp.Core.User.Repositories.Base;
using BlogApp.Core.User.Models;
using BlogApp.Core.Dtos.Models;

public class UserService : IUserService
{
    private readonly IUserRepository repository;

    public UserService(IUserRepository repository)
    {
        this.repository = repository;
    }
    public async Task CreateAsync(RegistrationDto user)
    {
        if(user is null || user.Email is null)
        {
            throw new ArgumentNullException("user must have an email");
        }

        await repository.CreateAsync(new User(){
            Email = user.Email,
            Name = user.Name,
        }, null);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if(string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("email cannot be null or empty");
        }

        return await repository.GetByEmailAsync(email);
    }

    public async Task<User?> GetSignedUpUser(LoginDto user)
    {
        if(user is null || user.Email is null)
        {
            throw new ArgumentNullException("user must have an email to login");
        }

        return await repository.GetSignedUpUser(user);
    }
}
