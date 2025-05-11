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
            return await _context.Products.ToListAsync(token);
        }

        public async Task<Product?> GetProductAsync(Guid id, CancellationToken token)
        {
            return await _context.Products.FindAsync(id, token);
        }
    }
}
