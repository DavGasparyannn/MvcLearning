using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business.Models.Shop;
using MvcLearning.Business.Services;
using MvcLearning.Data.Entities;

namespace MvcLearning.Controllers
{
    [Authorize(Roles = "ShopOwner")]
    public class ShopController : Controller
    {
        private readonly ShopService _shopService;
        private readonly OrderService _orderService;
        private readonly UserManager<User> _userManager;
        public ShopController(ShopService shopService,OrderService orderService,UserManager<User> userManager)
        {
            _shopService = shopService;
            _orderService = orderService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var shop = _shopService.GetShopAsync(user!.Id).Result;
            return View(shop);
        }
        public async Task<IActionResult> Orders(Guid shopId,CancellationToken token)
        {
            var orders = await _orderService.GetOrdersByShopIdAsync(shopId, token);
            return View(orders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShopCreatingModel model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _shopService.CreateShopAsync(model, userId);
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _shopService.DeleteShopAsync(id, userId);
                return RedirectToAction("Index");
            }   
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
