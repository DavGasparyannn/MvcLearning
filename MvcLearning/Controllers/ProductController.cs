using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business.Models.Product;
using MvcLearning.Business.Services;

namespace MvcLearning.Controllers
{
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
        public async Task<IActionResult> Create(ProductAddingModel model, Guid shopId, CancellationToken token = default)
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
    }
}
