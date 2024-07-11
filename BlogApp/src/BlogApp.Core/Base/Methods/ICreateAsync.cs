using Microsoft.AspNetCore.Http;

namespace BlogApp.Core.Base.Methods;


public interface ICreateAsync<TEntity>
{
    public Task CreateAsync(TEntity entity, IFormFile image);
}
