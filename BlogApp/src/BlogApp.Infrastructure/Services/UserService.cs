using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BlogApp.Core.Dtos;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;

namespace BlogApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task CreateAsync(RegistrationDto user)
        {
            if(user is null || user.Email is null)
            {
                throw new ArgumentException("user must have an email");
            }

            await repository.CreateAsync(new User(){
                Email = user.Email,
                Name = user.Name,
            });
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await repository.GetByEmailAsync(email);
        }

        public async Task<User?> GetSignedUpUser(LoginDto user)
        {
            if(user is null || user.Email is null)
            {
                throw new ArgumentException("user must have an email to login");
            }

            return await repository.GetSignedUpUser(user);
        }
    }
}