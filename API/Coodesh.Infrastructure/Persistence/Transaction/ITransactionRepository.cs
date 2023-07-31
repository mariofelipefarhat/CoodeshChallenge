using Coodesh.Infrastructure.Common;
using Coodesh.Infrastructure.Models.Transaction;

namespace Coodesh.Infrastructure.Persistence.Transaction;

public interface ITransactionRepository : IRepository<TransactionModel>
{
    void AddRange(List<TransactionModel> entities);
}
