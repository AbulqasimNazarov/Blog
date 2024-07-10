namespace BlogApp.Core.Role.Services.Base;

using BlogApp.Core.Role.Models;

public interface IRoleService
{
    public Task<Role?> GetByNameAsync(string name);
    public Task CreateAsync(Role role);
    public Task<Role?> IGetByIdAsync(int id);
}
