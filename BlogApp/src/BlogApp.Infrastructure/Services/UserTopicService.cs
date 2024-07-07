using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class UserTopicService : IUserTopicService
    {
        private readonly IUserTopicRepository repository;

        public UserTopicService(IUserTopicRepository repository)
        {
            this.repository = repository;
        }
        public async Task AddTopicToInterestAsync(UserTopic userTopic)
        {
            if(userTopic ==  null)
            {
                throw new ArgumentNullException("userTopic is null");
            }

            await repository.CreateAsync(userTopic);
        }

        public async Task DeleteTopicFromInterestAsync(UserTopic userTopic)
        {
            if(userTopic ==  null)
            {
                throw new ArgumentNullException("userTopic is null");
            }

            await repository.DeleteAsync(userTopic);
        }

        public async Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId)
        {
            return await repository.GetAllTopicsByUserId(userId);
        }
    }
}