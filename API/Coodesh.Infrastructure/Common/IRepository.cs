namespace Coodesh.Infrastructure.Common;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll();
    void Add(TEntity entity);
    void Add(List<TEntity> entities);
    int SaveChanges();
}
