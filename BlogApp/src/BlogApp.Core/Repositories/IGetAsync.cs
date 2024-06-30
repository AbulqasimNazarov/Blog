namespace BlogApp.Core.Repositories;

public interface IGetAsync<TEntity>
{
    public Task<TEntity> GetByIdAsync(Guid id);
}
