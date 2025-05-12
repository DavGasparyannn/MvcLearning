using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities;

namespace MvcLearning.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderFromBucket(Bucket bucket,CancellationToken token);
        Task<OrderDetailsDTO?> GetOrderByIdAsync(Guid orderId, CancellationToken token);
        Task<List<OrderDetailsDTO>?> GetOrdersByUserIdAsync(string userId, CancellationToken token);
    }
}
