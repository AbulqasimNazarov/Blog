namespace BlogApp.Core.Repositories;

public interface IUpdateAsync<TEntity>
{
     public Task UpdateByIdAsync(Guid id);    
}
