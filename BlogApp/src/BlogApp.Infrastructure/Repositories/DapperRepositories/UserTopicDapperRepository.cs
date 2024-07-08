using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.Repositories.DapperRepositories;

public class UserTopicDapperRepository : IUserTopicRepository
{
    private readonly string connectionString;
    public UserTopicDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task CreateAsync(UserTopic userTopic)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into UserTopics
                                    (UserId, TopicId) values (@UserId, @TopicId)", userTopic);
    }

    public async Task DeleteAsync(UserTopic userTopic)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"delete from UserTopics
                                    where UserId = @UserId, TopicId = @TopicId)", userTopic);
    }

    public async Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId)
    {
        var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select T.Id, T.Name from Topics T join UserTopics U on U.TopicId = T.Id where UserId = @UserId", new {
            UserId = userId,
        });

        return topics;
    }
}
