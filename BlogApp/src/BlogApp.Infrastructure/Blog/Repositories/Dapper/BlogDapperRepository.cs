using Dapper;
using Npgsql;


namespace BlogApp.Infrastructure.Blog.Repositories.Dapper;

using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Core.Blog.Models;
using Microsoft.AspNetCore.Http;
using BlogApp.Core.User.Repositories.Base;

public class BlogDapperRepository : IBlogRepository
{
    private readonly string connectionString;
    private readonly IUserRepository userRepository;
    public BlogDapperRepository(string connectionString, IUserRepository userRepository)
    {
        this.connectionString = connectionString;
        this.userRepository = userRepository;
    }

    public async Task<Blog> GetLastBlogAsync()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var query = "SELECT * FROM Blogs ORDER BY Id DESC LIMIT 1";
            return await connection.QueryFirstOrDefaultAsync<Blog>(query);
        }
    }


    public async Task CreateAsync(Blog blog, IFormFile image)
    {
        var lastBlog = await GetLastBlogAsync();
        int newId = lastBlog == null ? 1 : lastBlog.Id + 1;
        var extension = new FileInfo(image.FileName).Extension.Substring(1);
        blog.PictureUrl = $"Assets/IMG/{newId}.{extension}";


        using (var newFileStream = System.IO.File.Create(blog.PictureUrl))
        {
            await image.CopyToAsync(newFileStream);
        }

        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.ExecuteAsync(@"
        INSERT INTO Blogs (Title, Text, TopicId, UserId, PictureUrl) 
        VALUES (@Title, @Text, @TopicId, @UserId, @PictureUrl)",
                new
                {
                    blog.Title,
                    blog.Text,
                    blog.TopicId,
                    UserId = 40, 
                    blog.PictureUrl
                });
        }
    }


    public async Task<IEnumerable<Blog?>> GetAllByName(string name) // this methods finds various blogs those contain 'name' in the title  
    {
        var connection = new NpgsqlConnection(connectionString);

        Func<string, string> encodeForILike = word => word.Replace("[", "[[]").Replace("%", "[%]");

        var nameDapper = "%" + encodeForILike(name) + "%";

        var blogs = await connection.QueryAsync<Blog>(
            @"SELECT * FROM Blogs
            WHERE Title ILIKE @name;", new
            {
                name = nameDapper,
            });

        return blogs;
    }

    public async Task<IEnumerable<Blog?>> GetAllByTopicId(List<int> topicIds)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var query = @"SELECT * FROM Blogs WHERE TopicId = ANY(@TopicIds)";
        var blogs = await connection.QueryAsync<Blog>(query, new { TopicIds = topicIds });
        return blogs;
    }


    public async Task<IEnumerable<Blog?>> GetAllByUserId(int userId)
    {
        var connection = new NpgsqlConnection(connectionString);
        var blogs = await connection.QueryAsync<Blog>(@"select * from Blogs where UserId = @UserId", new
        {
            UserId = userId,
        });

        return blogs;
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        var connection = new NpgsqlConnection(connectionString);
        var blogs = await connection.QueryAsync<Blog>(@"select * from Blogs where @Id = Id", new
        {
            Id = id,
        });

        return blogs.FirstOrDefault();
    }
}
