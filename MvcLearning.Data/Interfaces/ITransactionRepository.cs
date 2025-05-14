using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;
using MvcLearning.Data.Entities;

namespace MvcLearning.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddMoneyToUserById(string userId, decimal amount, string? description = null, CancellationToken token = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default);
        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken token = default);
        Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken token = default);
        Task AddTransactionAsync(Transaction transaction, CancellationToken token = default);

    }
}
