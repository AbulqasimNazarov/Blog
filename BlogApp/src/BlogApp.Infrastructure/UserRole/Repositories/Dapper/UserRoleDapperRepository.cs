using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.UserRole.Repositories.Dapper;

using BlogApp.Core.UserRole.Repositories.Base;
using BlogApp.Core.UserRole.Models;
using BlogApp.Core.Role.Models;

public class UserRoleDapperRepository : IUserRoleRepository
{
    private readonly string connectionString;
    public UserRoleDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public Task ChangeAsync(int userId, Role role)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(UserRole userRole)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into UserRoles
                                (UserId, RoleId) values (@UserId, @RoleId)", userRole);
    }

    public async Task DeleteAsync(UserRole userRole)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"delete from UserRoles
                                where UserId = @UserId, RoleId = @RoleId)", userRole);
    }

    public async Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId)
    {
        var connection = new NpgsqlConnection(connectionString);
        var roles = await connection.QueryAsync<Role>(@"select R.Id, R.Name from Roles R join UserRoles U on U.RoleId = R.Id where UserId = @UserId", new {
            UserId = userId,
        });

        return roles;
    }
}
