using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public TransactionRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task AddMoneyToUserById(string userId, decimal amount, string? description = null, CancellationToken token = default)
        {
            await using var dbTransaction = await _context.Database.BeginTransactionAsync(token);
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    throw new Exception("User not found");

                user.Balance += amount;
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Type = Enums.TransactionType.Deposit,
                    UserId = user.Id,
                    Description = description ?? $"Deposit of amount {amount}",
                    Amount = amount,
                    CreatedAt = DateTime.Now
                };
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync(token);
                await dbTransaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw new Exception("Error while adding money to user", ex);
                throw;

            }
        }
            // Начать транзакцию
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default)
        {
            return await _context.Database.BeginTransactionAsync(token);
        }

        // Коммитить транзакцию
        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken token = default)
        {
            if (transaction != null)
            {
                await transaction.CommitAsync(token);
            }
        }

        // Откатить транзакцию
        public async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken token = default)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync(token);
            }
        }

        // Добавление транзакции в базу
        public async Task AddTransactionAsync(Transaction transaction, CancellationToken token = default)
        {
            await _context.Transactions.AddAsync(transaction, token);
        }

    }
}

