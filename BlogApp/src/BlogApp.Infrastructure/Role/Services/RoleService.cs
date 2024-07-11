namespace BlogApp.Infrastructure.Role.Services;

using BlogApp.Core.Role.Services.Base;
using BlogApp.Core.Role.Repositories.Base;
using BlogApp.Core.Role.Models;
using Microsoft.AspNetCore.Http;

public class RoleService : IRoleService
{
    private readonly IRoleRepository repository;

    public RoleService(IRoleRepository repository)
    {
        this.repository = repository;
    }
    public async Task CreateAsync(Role role, IFormFile image)
    {
        if(role is null || role.Name is null)
        {
            throw new ArgumentNullException("role must have a name");
        }

        await repository.CreateAsync(role, null);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        if(name is null)
        {
            throw new ArgumentNullException("name must contain something");
        }

        return await repository.GetByNameAsync(name);
    }

    public async Task<Role?> IGetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }
}
