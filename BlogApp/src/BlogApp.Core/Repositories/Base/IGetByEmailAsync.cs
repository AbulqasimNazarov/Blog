namespace BlogApp.Core.Repositories.Base;

public interface IGetByEmailAsync<TEntity>
{
    public Task<TEntity> GetByEmailAsync(string email);
}
