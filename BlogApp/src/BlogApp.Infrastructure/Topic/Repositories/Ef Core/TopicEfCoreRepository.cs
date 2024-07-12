#pragma warning disable CS1998

namespace BlogApp.Infrastructure.Topic.Repositories.Ef_Core;

using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Core.Topic.Models;
using BlogApp.Core.Topic.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MyGames.Infrastructure.Data.DbContext;

public class TopicEfCoreRepository : ITopicRepository
{
    private readonly BlogDbContext dbContext;

    public TopicEfCoreRepository(BlogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Topic?>> GetAllAsync()
    {
        return dbContext.Topics;
    }

    public async Task<Topic?> GetByIdAsync(int id)
    {
        return await dbContext.Topics.Where(t => t.Id == id).FirstOrDefaultAsync();
    }
}
