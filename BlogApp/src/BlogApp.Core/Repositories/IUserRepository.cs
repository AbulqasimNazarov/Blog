using BlogApp.Core.Models;

namespace BlogApp.Core.Repositories;

public interface IUserRepository : ICreateAsync<User>, IGetAsync<User>, IUpdateAsync<User>, IDeleteAsync<User>
{
    
    
}
