using BlogApp.Core.Models;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services;


public class UserConsoleService : IUserService
{
    public User GetUserById(Guid id){
        System.Console.WriteLine("Hello");
        return null;
    }
}
