using Coodesh.Infrastructure.Common;
using Coodesh.Infrastructure.Models;

namespace Coodesh.Infrastructure.Persistence.Product;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(TransactionContext ctx) : base(ctx)
    {

    }

    void ITransactionRepository.AddRange(List<Transaction> entities)
    {
        foreach (var entity in entities)
        {
            var existingEntity = _ctx.Set<Transaction>().FirstOrDefault(e => e.Type == entity.Type && e.Product == entity.Product && e.Date == entity.Date);

            if (existingEntity != null)
            {
                existingEntity.Update(entity.Type, entity.Date, entity.Product, entity.Amount, entity.Seller);
                _ctx.Set<Transaction>().Update(existingEntity);
            }
            else
            {
                _ctx.Set<Transaction>().Add(entity);
            }
        }
    }
}
