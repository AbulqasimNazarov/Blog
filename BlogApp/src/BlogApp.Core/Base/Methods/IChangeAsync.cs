namespace BlogApp.Core.Base.Methods;

public interface IChangeAsync<TEntity>
{
    public Task ChangeAsync(int id, TEntity entity);
}
