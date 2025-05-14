using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business.Services
{
    public class BucketService
    {
        private readonly IBucketRepository _bucketRepository;
        private readonly IProductRepository _productRepository;
        public BucketService(IBucketRepository bucketRepository,IProductRepository productRepository)
        {
            _bucketRepository = bucketRepository;
            _productRepository = productRepository;
        }
        public async Task AddBucketToUser(string userId, CancellationToken token = default)
        {
            await _bucketRepository.AddBucketToUser(userId, token);
        }
        public async Task<Bucket> GetBucketAsync(string userId, CancellationToken token = default)
        {
            return await _bucketRepository.GetBucketByUserId(userId, token);
        }
        public async Task AddProductToBucket(string userId, Guid productId, int quantity, CancellationToken token = default)
        {
            var bucket = await GetBucketAsync(userId, token);

            await _productRepository.AddProductToBucket(bucket.Id, productId, quantity, token);
        }

        public async Task<(bool Success, int NewQuantity, decimal Subtotal, decimal Total, string Message)> SetProductQuantityAsync(string userId, Guid productId, int quantity,CancellationToken token = default)
        {
            if (quantity < 1) return (false, 0, 0, 0, "Invalid quantity");

            var bucket = await _bucketRepository.GetBucketByUserId(userId,token);
            var item = bucket?.BucketProducts?.FirstOrDefault(bp => bp.ProductId == productId);
            if (item == null) return (false, 0, 0, 0, "Item not found");

            item.Quantity = quantity;
            await _bucketRepository.SaveChangesAsync();

            var subtotal = (item.Product?.Price ?? 0) * quantity;
            var total = bucket.BucketProducts.Sum(bp => (bp.Product?.Price ?? 0) * bp.Quantity);

            return (true, quantity, subtotal, total, "Updated");
        }
    }
}
