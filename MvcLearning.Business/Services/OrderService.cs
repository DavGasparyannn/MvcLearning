using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBucketRepository _bucketRepository;
        public OrderService(IOrderRepository orderRepository, IBucketRepository bucketRepository)
        {
            _orderRepository = orderRepository;
            _bucketRepository = bucketRepository;
        }
        public async Task<Order> CreateOrderFromUserBucketAsync(string userId,CancellationToken token = default)
        {
            var bucket = await _bucketRepository.GetBucketByUserId(userId, token);
            var order = await _orderRepository.CreateOrderFromBucket(bucket, token);
            bucket.BucketProducts.Clear();
            await _bucketRepository.UpdateBucketAsync(bucket, token);
            return order;
        }

        public async Task<OrderDetailsDTO?> GetOrderByIdAsync(Guid orderId, CancellationToken token)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId, token);
        }

        public async Task<string?> GetOrdersByShopIdAsync(Guid shopId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDetailsDTO>?> GetOrdersByUserIdAsync(string userId, CancellationToken token)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId, token);
        }
    }
}
