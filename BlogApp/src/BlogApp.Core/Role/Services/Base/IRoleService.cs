namespace BlogApp.Core.Role.Services.Base;

using BlogApp.Core.Role.Models;
using Microsoft.AspNetCore.Http;

public interface IRoleService
{
    public Task<Role?> GetByNameAsync(string name);
    public Task CreateAsync(Role role, IFormFile image);
    public Task<Role?> IGetByIdAsync(int id);
}
