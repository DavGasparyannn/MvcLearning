using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business;
using MvcLearning.Data.Entities;

namespace MvcLearning.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly ShopService _shopService;
        private readonly UserManager<User> _userManager;
        public ShopController(ShopService shopService,UserManager<User> userManager)
        {
            _shopService = shopService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var shop = _shopService.GetShopAsync(user!.Id).Result;
            return View(shop);
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
    }
}
