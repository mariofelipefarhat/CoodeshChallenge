using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coodesh.Application.Interfaces
{
    public interface ITransactionFileProcessorService
    {
        ErrorOr<bool> ProcessFile(Stream fileStream);
    }
}
