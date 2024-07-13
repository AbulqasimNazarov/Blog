using Dapper;
using Npgsql;

namespace BlogApp.Infrastructure.Blog.Repositories.Dapper;

using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Core.Blog.Models;

public class BlogDapperRepository : IBlogRepository
{
    private readonly string connectionString;
    public BlogDapperRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task CreateAsync(Blog blog)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync(@"insert into ""Blogs""
                                    (""Title"", ""Text"", ""TopicId"", ""UserId"", ""PictureUrl"") 
                                    values (@Title, @Text, @TopicId, @UserId, @PictureUrl)",
                                    blog);
    }

    public async Task<IEnumerable<Blog?>> GetAllByName(string name) // this methods finds various blogs those contain 'name' in the title  
    {
        using var connection = new NpgsqlConnection(connectionString);

        Func<string, string> encodeForILike = word => word.Replace("[", "[[]").Replace("%", "[%]");

        var nameDapper = "%" + encodeForILike(name) + "%";

        var blogs = await connection.QueryAsync<Blog>(
            @"SELECT * FROM ""Blogs""
            WHERE ""Title"" ILIKE @name;", new {
            name = nameDapper,
        });

        return blogs;
    }

    public async Task<IEnumerable<Blog?>> GetAllByTopicId(int topicId) 
    {
        using var connection = new NpgsqlConnection(connectionString);
        var blogs = await connection.QueryAsync<Blog>(@"select * from ""Blogs"" where ""TopicId"" = @TopicId", new {
            TopicId = topicId,
        });

        return blogs;
    }

    public async Task<IEnumerable<Blog?>> GetAllByUserId(int userId)
    {
        var connection = new NpgsqlConnection(connectionString);
        var blogs = await connection.QueryAsync<Blog>(@"select * from Blogs where UserId = @UserId", new {
            UserId = userId,
        });

        return blogs;
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var blogs = await connection.QueryAsync<Blog>(@"select * from Blogs where @Id = Id", new {
            Id = id,
        });

        return blogs.FirstOrDefault();
    }
}