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
            return await _context.Shops.ToListAsync(token);
        }
        public async Task<Shop> GetShopByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Shops.FindAsync(id,token) ?? throw  new Exception("Shop does not found");
        }
        public async Task<Shop> GetShopByUserIdAsync(string userId, CancellationToken token)
        {
            return await _context.Shops.FirstOrDefaultAsync(s => s.OwnerId == userId, token) ?? throw new Exception("Shop does not found");
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
        public async Task<bool> ShopExistsAsync(Guid id, CancellationToken token)
        {
            return await _context.Shops.AnyAsync(s => s.Id == id, token);
        }



    }
}
