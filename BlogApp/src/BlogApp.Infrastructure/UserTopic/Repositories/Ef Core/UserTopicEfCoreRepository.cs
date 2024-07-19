#pragma warning disable CS1998

namespace BlogApp.Infrastructure.UserTopic.Repositories.Ef_Core;

using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.UserTopic.Models;
using BlogApp.Core.UserTopic.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using BlogApp.Infrastructure.Data.DbContext;

public class UserTopicEfCoreRepository : IUserTopicRepository
{
    private readonly BlogDbContext dbContext;

    public UserTopicEfCoreRepository(BlogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(UserTopic userTopic)
    {
        await dbContext.UserTopics.AddAsync(userTopic);
        await dbContext.SaveChangesAsync();
    }

    public async Task CreateListAsync(IEnumerable<UserTopic> userTopics)
    {
        await dbContext.UserTopics.AddRangeAsync(userTopics);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserTopic userTopic)
    {
        dbContext.UserTopics.Remove(userTopic);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Topic?>> GetAllTopicsByUserIdAsync(int userId)
    {
        return dbContext.UserTopics
            .Where(ut => ut.UserId == userId)
            .Include(ut => ut.Topic)
            .Select(ut => ut.Topic);
    }
}
