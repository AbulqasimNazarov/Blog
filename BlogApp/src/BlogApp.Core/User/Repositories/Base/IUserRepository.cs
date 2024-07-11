namespace BlogApp.Core.User.Repositories.Base;

using BlogApp.Core.User.Models;
using BlogApp.Core.Dtos.Models;
using BlogApp.Core.Base.Methods;

public interface IUserRepository: ICreateAsync<User?>, IGetByIdAsync<User?>
{      
    public Task<User?> GetSignedUpUser(LoginDto user);
    public Task<User?> GetByEmailAsync(string email);
}
