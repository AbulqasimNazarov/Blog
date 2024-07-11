namespace BlogApp.Core.UserRole.Services.Base;

using BlogApp.Core.UserRole.Models;
using BlogApp.Core.Role.Models;

public interface IUserRoleService
{
    public Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId);
    public Task ChangeAsync(int userId, Role role);
    public Task CreateAsync(UserRole userRole);
    public Task DeleteAsync(UserRole userRole);
}
