using Coodesh.Infrastructure.Common;
using Coodesh.Infrastructure.Models.Transaction;

namespace Coodesh.Infrastructure.Persistence.Transaction;

public class TransactionRepository : Repository<TransactionModel>, ITransactionRepository
{
    public TransactionRepository(TransactionContext ctx) : base(ctx)
    {

    }

    void ITransactionRepository.AddRange(List<TransactionModel> entities)
    {
        foreach (var entity in entities)
        {
            var existingEntity = _ctx.Set<TransactionModel>().FirstOrDefault(e => e.Type == entity.Type && e.Product == entity.Product && e.Date == entity.Date);

            if (existingEntity != null)
            {
                existingEntity.Update(entity.Type, entity.Date, entity.Product, entity.Amount, entity.Seller);
                _ctx.Set<TransactionModel>().Update(existingEntity);
            }
            else
            {
                _ctx.Set<TransactionModel>().Add(entity);
            }
        }
    }
}
