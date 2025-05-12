using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product, CancellationToken token)
        {
            await _context.Products.AddAsync(product, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteProduct(Guid id, CancellationToken token)
        {
            var product = await GetProductAsync(id, token);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(token);
        }

        public async Task<List<Product>> GetAllProducts(CancellationToken token)
        {
            return await _context.Products
                .Include(p => p.Shop)
                .Include(p => p.Shop.Owner)
                .ToListAsync(token);
        }

        public async Task<Product?> GetProductAsync(Guid id, CancellationToken token)
        {
            return await _context.Products
                .Include(p => p.Shop)
                .Include(p => p.Shop.Owner)
                .FirstOrDefaultAsync(p => p.Id == id,token);
        }
        public async Task AddProductToBucket(Guid bucketId, Guid productId, int quantity = 1 ,CancellationToken token = default) {
            var existing = await _context.BucketProducts
         .FirstOrDefaultAsync(bp => bp.BucketId == bucketId && bp.ProductId == productId, token);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                var bucketProduct = new BucketProduct
                {
                    Id = Guid.NewGuid(),
                    BucketId = bucketId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.UtcNow
                };
                _context.BucketProducts.Add(bucketProduct);
            }

            await _context.SaveChangesAsync(token);
        }
    }
}
