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
                                    (UserId, RoleId) values (@UserId, RoleId)", userRole);
        }

        public Task DeleteAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role?>> GetAllRolesByUserIdAsync(int userId)
        {
            var connection = new NpgsqlConnection(connectionString);
            var roles = await connection.QueryAsync<Role>(@"select R.Id, R.Name from Roles R join UserRoles U on U.RoleId = R.Id", new {
                UserId = userId,
            });

            return roles;
        }
    }
}