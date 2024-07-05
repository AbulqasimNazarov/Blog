using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.Repositories.DapperRepositories
{
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
}