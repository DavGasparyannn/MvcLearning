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
    public class ShopRepository : IShopRepository 
    {
        private readonly ApplicationDbContext _context;
        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shop>> GetAllShopsAsync(CancellationToken token)
        {
            return await _context.Shops.Include(s => s.Products).ToListAsync(token);
        }
        public async Task<Shop> GetShopByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Shops
                .Include(s => s.Products)
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(s=>s.Id == id) ?? null;
        }
        public async Task<Shop> GetShopByUserIdAsync(string userId, CancellationToken token)
        {
            return await _context.Shops
                .Include(s => s.Products)
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(s => s.OwnerId == userId, token) ?? null;
        }
        public async Task<Shop> CreateShopAsync(Shop shop, CancellationToken token)
        {
            await _context.Shops.AddAsync(shop, token);
            await _context.SaveChangesAsync(token);
            return shop;
        }
        public async Task<Shop> UpdateShopAsync(Shop shop, CancellationToken token)
        {
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(token);
            return shop;
        }
        public async Task DeleteShopAsync(Guid id, CancellationToken token)
        {
            var shop = await GetShopByIdAsync(id, token);
            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync(token);
        }
        public async Task<bool> ShopExistsAsync(string name, CancellationToken token)
        {
            return await _context.Shops.AnyAsync(s => s.Name == name, token);
        }

        public async Task AddProductToShopAsync(Guid shopId, Product product, CancellationToken token)
        {
            var shop = await GetShopByIdAsync(shopId, token);
            if (shop == null)
                throw new InvalidOperationException("Shop not found.");
            if (product == null)
                throw new InvalidOperationException("Product not found.");

            shop.Products.Add(product);
            await _context.SaveChangesAsync(token);
        }
        public async Task<List<Order>> GetOrdersByShopId(Guid shopId, CancellationToken token)
        {
            var orders = await _context.Orders
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Include(o =>o.User)
        .Where(o => o.OrderItems.Any(oi => oi.Product.ShopId == shopId))
        .ToListAsync(token);
            foreach (var order in orders)
            {
                Console.WriteLine($"OrderId: {order.Id}");
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"  Product: {item.Product?.Name}, ShopId: {item.Product?.ShopId}");
                }
            }
            return orders;
        }
    }
}
