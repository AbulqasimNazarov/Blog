using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface IUserTopicService
    {
        public Task AddTopicToInterestAsync(UserTopic userTopic); // represents deleting topic to user interest
        public Task DeleteTopicFromInterestAsync(UserTopic userTopic); // represents adding topic to user interest
        public Task<IEnumerable<Topic?>> GetAllTopicsByUserId(int userId);
    }
}