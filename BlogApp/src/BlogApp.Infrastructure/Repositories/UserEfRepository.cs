using BlogApp.Core.Models;
using BlogApp.Core.Repositories;

namespace BlogApp.Infrastructure.Repositories;

public class UserEfRepository : IUserRepository
{
    public Task CreateUser()
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }


    public Task<User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
