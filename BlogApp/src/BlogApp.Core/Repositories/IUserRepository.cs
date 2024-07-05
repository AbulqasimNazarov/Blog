using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.Dtos;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories.Base;

namespace BlogApp.Core.Repositories
{
    public interface IUserRepository: ICreateAsync<User?>, IGetByIdAsync<User?>, IGetByEmailAsync<User?>
    {      
        public Task<User?> GetSignedUpUser(LoginDto user);
    }
}