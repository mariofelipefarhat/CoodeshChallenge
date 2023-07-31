using Coodesh.Infrastructure.Common;
using Coodesh.Infrastructure.Models;

namespace Coodesh.Infrastructure.Persistence.Product;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(TransactionContext ctx) : base(ctx)
    {

    }
}
