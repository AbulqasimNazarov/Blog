namespace BlogApp.Core.Base.Methods;

public interface IGetAllAsync<TEntity>
{
    public Task<IEnumerable<TEntity>> GetAllAsync();
}
