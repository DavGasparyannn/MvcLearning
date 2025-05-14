using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcLearning.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddMoneyToUserById(string userId, decimal amount, string? description = null, CancellationToken token = default);
    }
}
