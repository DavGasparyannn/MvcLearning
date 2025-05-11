using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities;

namespace MvcLearning.Data.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product shop, CancellationToken token);
        Task<Product?> GetProductAsync(Guid id, CancellationToken token);
    }
}
