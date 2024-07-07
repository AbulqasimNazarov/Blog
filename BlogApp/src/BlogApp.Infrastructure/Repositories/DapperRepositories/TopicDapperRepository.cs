using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.Repositories.DapperRepositories;

public class TopicDapperRepository : ITopicRepository
{
    private readonly string connectionString;
    public TopicDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public async Task<Topic?> GetByIdAsync(int id)
    {
        var connection = new NpgsqlConnection(connectionString);
        var roles = await connection.QueryAsync<Topic>(@"select * from Topics where @Id = Id", new {
            Id = id,
        });

        return roles.FirstOrDefault();
    }
}
