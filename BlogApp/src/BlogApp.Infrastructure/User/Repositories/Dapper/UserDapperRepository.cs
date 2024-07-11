#pragma warning disable CS8613

using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.User.Repositories.Dapper;

using BlogApp.Core.User.Repositories.Base;
using BlogApp.Core.User.Models;
using BlogApp.Core.Dtos.Models;
using Microsoft.AspNetCore.Http;

public class UserDapperRepository : IUserRepository
{
    private readonly string connectionString;
    public UserDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task CreateAsync(User? user, IFormFile image)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into Users
                                (Email, Name) values (@Email, @Name)", user);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var connection = new NpgsqlConnection(connectionString);
        var users = await connection.QueryAsync<User>(@"select * from Users where @Id = Id", new {
            Id = id,
        });

        return users.FirstOrDefault();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var connection = new NpgsqlConnection(connectionString);
        var users = await connection.QueryAsync<User>(@"select * from Users where @Email = email", new {
            Email = email,
        });

        return users.FirstOrDefault();
    }

    public async Task<User?> GetSignedUpUser(LoginDto userToFind)
    {
        var connection = new NpgsqlConnection(connectionString);
        var foundUsers = await connection.QueryAsync<User>(@"select * from Users where @Email = Email", userToFind);

        return foundUsers.FirstOrDefault();
    }
}

