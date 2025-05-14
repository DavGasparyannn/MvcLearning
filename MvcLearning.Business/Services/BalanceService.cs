using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business.Services
{
    public class BalanceService
    {
        private readonly ITransactionRepository _transactionRepository;
        public BalanceService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> DepositToBalanceAsync(string userId, decimal amount, string description,CancellationToken token = default)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            }
            try
            {
                await _transactionRepository.AddMoneyToUserById(userId, amount, description,token);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Deposit failed!");
            }
        }
    }
}
