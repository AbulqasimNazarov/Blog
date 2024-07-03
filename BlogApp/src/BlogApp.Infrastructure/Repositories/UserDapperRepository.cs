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

        public async Task<bool> isSignedUp(User userToFind)
        {
            var connection = new NpgsqlConnection(connectionString);
            var foundUsers = await connection.QueryAsync<User>(@"select * from Users where @Name = Name and @Email = Email", userToFind);
            var isSignedUp = foundUsers.FirstOrDefault() is null ? false : true;

            return isSignedUp;
        }
    }
}