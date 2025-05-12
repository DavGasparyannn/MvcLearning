using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities;

namespace MvcLearning.Data.Interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShopsAsync(CancellationToken token);
        Task<Shop> GetShopByIdAsync(Guid id, CancellationToken token);
        Task<Shop> GetShopByUserIdAsync(string userId, CancellationToken token);
        Task<Shop> CreateShopAsync(Shop shop, CancellationToken token);
        Task<Shop> UpdateShopAsync(Shop shop, CancellationToken token);
        Task DeleteShopAsync(Guid id, CancellationToken token);
        Task<bool> ShopExistsAsync(string id, CancellationToken token);
        Task AddProductToShopAsync(Guid shopId, Product product, CancellationToken token);
        Task<List<Order>> GetOrdersByShopId(Guid shopId, CancellationToken token);
    }
}
