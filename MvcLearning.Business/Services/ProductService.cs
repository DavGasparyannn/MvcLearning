using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Business.Models.Product;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        public ProductService(IProductRepository productRepository, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
        }
        public async Task AddProductAsync(ProductAddingModel productAddingModel,Guid shopId, CancellationToken token = default)
        {
            var product = new Product
            {
                Name = productAddingModel.Name,
                Description = productAddingModel.Description,
                Price = productAddingModel.Price,
                ImageUrls = productAddingModel.ImageUrls,
                ShopId = shopId,
            };
            await _productRepository.AddAsync(product, token);
            await _shopRepository.AddProductToShopAsync(shopId, product, token);

        }
    }
}
