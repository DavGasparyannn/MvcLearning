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
        public async Task<IActionResult> Create(string userId)
        {
            var shop = await _shopService.GetShopAsync(userId);
            if (shop == null || shop.OwnerId != userId)
            {
                return NotFound();
            }
            ViewData["ShopId"] = shop.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductAddingModel model, string userId, CancellationToken token = default)
        {
            if (!ModelState.IsValid)
            {
                var shop = await _shopService.GetShopAsync(userId);
                ViewData["ShopId"] = shop?.Id;
                ViewData["UserId"] = userId;
                return View(model);
            }

            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != currentUserId)
                {
                    return NotFound();
                }

                var shop = await _shopService.GetShopAsync(userId);
                if (shop == null)
                {
                    return NotFound();
                }

                await _productService.AddProductAsync(model, token); // Передаём shop.Id
                return RedirectToAction("Index", "Shop");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewData["UserId"] = userId;
                return View(model);
            }
        }
    }
}
