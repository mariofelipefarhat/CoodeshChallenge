using Coodesh.Infrastructure.Common;
using Coodesh.Infrastructure.Models;

namespace Coodesh.Infrastructure.Persistence.Product;

public interface ITransactionRepository : IRepository<Transaction>
{
    void AddRange(List<Transaction> entities);
}
