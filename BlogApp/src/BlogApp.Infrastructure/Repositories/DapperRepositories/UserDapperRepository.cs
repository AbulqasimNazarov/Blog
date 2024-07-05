using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Dtos;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Dapper;
using Npgsql;


#pragma warning disable CS8613

namespace BlogApp.Infrastructure.Repositories
{
    public class UserDapperRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserDapperRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateAsync(User? user)
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
}
