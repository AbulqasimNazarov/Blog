namespace BlogApp.Core.Repositories;

public interface IDeleteAsync<TEntity>
{
    public Task DeleteByIdAsync(Guid id);
}
