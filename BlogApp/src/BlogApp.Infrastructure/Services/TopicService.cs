using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository repository;

        public TopicService(ITopicRepository repository)
        {
            this.repository = repository;
        }

        

        public async Task<Topic?> IGetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}