namespace BlogApp.Core.Repositories;

public interface ICreateAsync<TEntity>
{
        public Task CreateUser();
}
