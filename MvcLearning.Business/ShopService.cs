using Microsoft.AspNetCore.Identity;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business
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
        public async Task CreateShop(ShopCreatingModel shopCreatingModel)
        {

        }
    }
}
