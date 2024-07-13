using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.UserTopic.Repositories.Dapper;

using BlogApp.Core.UserTopic.Repositories.Base;
using BlogApp.Core.UserTopic.Models;
using BlogApp.Core.Topic.Models;

public class UserTopicDapperRepository : IUserTopicRepository
{
    private readonly string connectionString;
    public UserTopicDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task CreateAsync(UserTopic userTopic)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into ""UserTopics""
                                    (""UserId"", ""TopicId"") values (@UserId, @TopicId)", userTopic);
    }

    public async Task DeleteAsync(UserTopic userTopic)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"delete from ""UserTopics""
                                    where ""UserId"" = @UserId, ""TopicId"" = @TopicId)", userTopic);
    }

    public async Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var topics = await connection.QueryAsync<Topic>(@"select T.""Id"", T.""Name"" from ""Topics"" T join ""UserTopics"" U on U.""TopicId"" = T.""Id"" where ""UserId"" = @UserId", new {
            UserId = userId,
        });

        return topics;
    }
}
