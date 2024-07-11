namespace BlogApp.Infrastructure.UserRole.Services;

using BlogApp.Core.UserRole.Services.Base;
using BlogApp.Core.UserRole.Repositories.Base;
using BlogApp.Core.UserRole.Models;
using BlogApp.Core.Role.Models;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository repository;

    public UserRoleService(IUserRoleRepository repository)
    {
        this.repository = repository;
    }
    public async Task ChangeAsync(int userId, Role role)
    {
        if(role is null || role.Name is null)
        {
            throw new ArgumentNullException("role must have a name");
        }

        await repository.ChangeAsync(userId, role);
    }

    public async Task CreateAsync(UserRole userRole)
    {
        if(userRole is null)
        {
            throw new ArgumentNullException("userRole must contain something");
        }

        await repository.CreateAsync(userRole);
    }

    public async Task DeleteAsync(UserRole userRole)
    {
        if(userRole is null)
        {
            throw new ArgumentNullException("userRole must contain something");
        }

        await repository.DeleteAsync(userRole);
    }

    public async Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId)
    {
        if(userId <= 0)
        {
            throw new ArgumentException("userId cannot be zero or below");
        }
        
        return await repository.GetAllRolesByUserIdAsync(userId);
    }
}
