using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MyGames.Infrastructure.Data.DbContext;

using BlogApp.Core.Role.Models;
using BlogApp.Core.User.Models;
using BlogApp.Core.Blog.Models;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.UserTopic.Models;

using BlogApp.Core.User.Data.Configurations;
using BlogApp.Core.Blog.Data.Configurations;
using BlogApp.Core.Topic.Data.Configurations;
using BlogApp.Core.UserTopic.Data.Configurations;
using Microsoft.Extensions.Configuration;

public class BlogDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Topic> Topics { get; set; }
    public DbSet<UserTopic> UserTopics { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public BlogDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlogConfiguration());
        modelBuilder.ApplyConfiguration(new UserTopicConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}