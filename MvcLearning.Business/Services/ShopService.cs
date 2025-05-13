using Microsoft.AspNetCore.Identity;
using MvcLearning.Business.Models.Shop;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Enums;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business.Services
{
    public class ShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly UserManager<User> _userManager;
        public ShopService(IShopRepository shopRepository, UserManager<User> userManager)
        {
            _shopRepository = shopRepository;
            _userManager = userManager;
        }
        /*public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _shopRepository.GetAllProductsAsync();
        }*/
        public async Task CreateShopAsync(ShopCreatingModel shopCreatingModel, string ownerId, CancellationToken token = default)
        {
            if (await _shopRepository.ShopExistsAsync(shopCreatingModel.Name,token))
            {
                throw new Exception("Shop with this name already exists");
            }
            var user = await _userManager.FindByIdAsync(ownerId);
            if (user!.Shop != null)
                throw new InvalidOperationException("You are currentrly have a shop.");
            
            var shop = new Shop
            {
                Name = shopCreatingModel.Name,
                Description = shopCreatingModel.Description,
                ImageUrl = shopCreatingModel.ImageUrl,
                OwnerId = ownerId,
                CreatedAt = DateTime.UtcNow
            };
            
            await _shopRepository.CreateShopAsync(shop, token);
        }
        public async Task DeleteShopAsync(Guid shopId, string ownerId,CancellationToken token = default)
        {
            var shop = await _shopRepository.GetShopByIdAsync(shopId, token);
            if (shop == null)
                throw new InvalidOperationException("Shop not found.");
            if (shop.OwnerId != ownerId)
                throw new InvalidOperationException("You are not the owner of this shop.");

            await _shopRepository.DeleteShopAsync(shopId, token);
        }
        public async Task<Shop> GetShopAsync(Guid shopId)
        {
            var shop = await _shopRepository.GetShopByIdAsync(shopId, CancellationToken.None);
            return shop;
        }
        public async Task<Shop> GetShopAsync(string userId)
        {
            var shop = await _shopRepository.GetShopByUserIdAsync(userId, CancellationToken.None);
            return shop;
        }

        public async Task<bool> UpdateOrderStatus(Guid orderId, OrderStatus newStatus,string shopOwnerId ,CancellationToken token)
        {
           return await _shopRepository.UpdateOrderStatus(orderId, newStatus,shopOwnerId, token);
        }
    }
}
