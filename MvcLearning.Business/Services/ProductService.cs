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

        public async Task<Product?> GetProduct(Guid productId,CancellationToken token = default)
        {
            return await _productRepository.GetProductAsync(productId,token);
        
        }
        public async Task Delete(Guid productId,CancellationToken token = default)
        {
            await _productRepository.DeleteProduct(productId,token);
        }
        public async Task<List<Product>> GetAllProducts(CancellationToken token = default)
        {
            var products = await _productRepository.GetAllProducts(token);
            return (List<Product>)products.Take(10);
        }
    }
}
