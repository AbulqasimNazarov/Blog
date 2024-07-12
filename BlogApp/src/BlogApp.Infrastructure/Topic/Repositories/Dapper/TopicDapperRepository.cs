using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.Topic.Repositories.Dapper;

using BlogApp.Core.Topic.Repositories.Base;
using BlogApp.Core.Topic.Models;
using System.Collections.Generic;

public class TopicDapperRepository : ITopicRepository
{
    private readonly string connectionString;
    public TopicDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<IEnumerable<Topic?>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select * from Topics");

        return topics;
    }

    public async Task<Topic?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select * from Topics where @Id = Id", new {
            Id = id,
        });

        return topics.FirstOrDefault();
    }
}
