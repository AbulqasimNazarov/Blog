using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;

namespace BlogApp.Core.Services
{
    public interface ITopicService
    {
        public Task<Topic?> IGetByIdAsync(int id);
    }
}