using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.Role.Repositories.Dapper;

using BlogApp.Core.Role.Repositories.Base;
using BlogApp.Core.Role.Models;

public class RoleDapperRepository : IRoleRepository
{
    private readonly string connectionString;
    public RoleDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public async Task CreateAsync(Role role)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into Roles
                                (Name) values (@Name)", role);
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        var connection = new NpgsqlConnection(connectionString);
        var roles = await connection.QueryAsync<Role>(@"select * from Roles where @Id = Id", new {
            Id = id,
        });

        return roles.FirstOrDefault();
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        var connection = new NpgsqlConnection(connectionString);
        var roles = await connection.QueryAsync<Role>(@"select * from Roles where @Id = Id", new {
            Name = name,
        });

        return roles.FirstOrDefault();
    }
}
