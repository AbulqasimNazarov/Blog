namespace BlogApp.Core.UserRole.Repositories.Base;

using BlogApp.Core.UserRole.Models;
using BlogApp.Core.Base.Methods;
using BlogApp.Core.Role.Models;

public interface IUserRoleRepository : ICreateAsync<UserRole>, IDeleteAsync<UserRole>
{
    public Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId);
    public Task ChangeAsync(int userId, Role role);
}
