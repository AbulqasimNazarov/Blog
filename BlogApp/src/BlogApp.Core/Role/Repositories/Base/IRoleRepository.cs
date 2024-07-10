namespace BlogApp.Core.Role.Repositories.Base;

using BlogApp.Core.Role.Models;
using BlogApp.Core.Base.Methods;

public interface IRoleRepository : ICreateAsync<Role>, IGetByIdAsync<Role?>
{
    public Task<Role?> GetByNameAsync(string name);
}
