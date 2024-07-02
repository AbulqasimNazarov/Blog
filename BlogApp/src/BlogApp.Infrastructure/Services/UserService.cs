using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IUserRepository repository;
        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task CreateAsync(User user)
        {
            if(user is null || user.Name is null || user.Surname is null || user.Username is null || user.Email is null)
            {
                throw new ArgumentNullException("user or property of the user contains null");
            }

            await repository.CreateAsync(user);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}