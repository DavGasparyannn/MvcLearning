using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Business.Models.Product;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Entities.Images;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly ImageService _imageService;
        public ProductService(IProductRepository productRepository, IShopRepository shopRepository, ImageService imageService)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _imageService = imageService;
        }
        public async Task AddProductAsync(ProductModel productAddingModel, Guid shopId, CancellationToken token = default)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productAddingModel.Name,
                Description = productAddingModel.Description,
                Price = productAddingModel.Price,
                ShopId = shopId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Images = new List<ProductImage>()
            };

            if (productAddingModel.UploadedImages != null)
            {
                if (productAddingModel.UploadedImages.Count > 5)
                    throw new InvalidOperationException("There are 5 images maximum");

                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", shopId.ToString(), product.Id.ToString());
                Directory.CreateDirectory(imagesPath);


                await _imageService.AddImageToProduct(productAddingModel, imagesPath, product, shopId.ToString(), token);
            }
            await _productRepository.AddAsync(product, token);
            await _shopRepository.AddProductToShopAsync(shopId, product, token);
        }

        public async Task<Product?> GetProduct(Guid productId, CancellationToken token = default)
        {
            return await _productRepository.GetProductAsync(productId, token);

        }
        public async Task Delete(Guid productId, CancellationToken token = default)
        {
            await _productRepository.DeleteProduct(productId, token);
        }
        public async Task<List<Product>> GetAllProducts(CancellationToken token = default)
        {
            var products = await _productRepository.GetAllProducts(token);
            return products.Take(10).ToList();
        }

        public async Task<bool> UpdateProductAsync(ProductModel model, CancellationToken token = default)
        {
            var existing = await _productRepository.GetProductAsync(model.Id, token);
            if (existing == null)
                return false;

            // Маппим данные из модели в сущность
            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Price = model.Price;
            // и т.д.

            return await _productRepository.UpdateProductAsync(existing, token);
        }
    }
}
