using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business.Models.Product;
using MvcLearning.Business.Services;

namespace MvcLearning.Controllers
{
    [Authorize(Roles = "ShopOwner")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ShopService _shopService;
        public ProductController(ProductService productService, ShopService shopService)
        {
            _productService = productService;
            _shopService = shopService;
        }
        public async Task<IActionResult> Create(Guid shopId)
        {
            var shop = await _shopService.GetShopAsync(shopId);
            
            ViewData["ShopId"] = shop.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel model, Guid shopId, CancellationToken token = default)
        {
                var shop = await _shopService.GetShopAsync(shopId);
            if (!ModelState.IsValid)
            {
                ViewData["ShopId"] = shop?.Id;
                ViewData["UserId"] = shop!.OwnerId;
                return View(model);
            }

            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ownerId = shop.OwnerId;
                if (ownerId != currentUserId)
                {
                    return NotFound();
                }

                if (shop == null)
                {
                    return NotFound();
                }

                await _productService.AddProductAsync(model,shopId, token); // Передаём shop.Id
                return RedirectToAction("Index", "Shop");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewData["UserId"] = shop.OwnerId;
                return View(model);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid productId)
        {
           var product = await _productService.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }   
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _productService.Delete(productId);
                return RedirectToAction("Index", "Shop");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index","Shop");
            }
        }
        public async Task<IActionResult> Edit(Guid productId)
        {
            var product = await _productService.GetProduct(productId);
            if (product == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrls = product.ImageUrls
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel updatedProduct)
        {
            if (!ModelState.IsValid)
                return View(updatedProduct);

            var result = await _productService.UpdateProductAsync(updatedProduct);

            if (!result)
                return NotFound(); // или вернуть ошибку

            return Redirect($"/Product/Details?productId={updatedProduct.Id}");
        }
    }
}
