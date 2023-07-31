using Microsoft.EntityFrameworkCore;

namespace Coodesh.Infrastructure.Common;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly TransactionContext _ctx;
    public Repository(TransactionContext ctx) => _ctx = ctx;
    public async Task<IEnumerable<TEntity>> GetAll() => await _ctx.Set<TEntity>().ToListAsync();
    public void Add(TEntity entity) => _ctx.Set<TEntity>().Add(entity);
    public int SaveChanges() => _ctx.SaveChanges();
}
