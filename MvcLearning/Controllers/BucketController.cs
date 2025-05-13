using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business.Services;
using MvcLearning.Data.Entities;

namespace MvcLearning.Controllers
{
    [Authorize]
    public class BucketController : Controller
    {
        private readonly BucketService _bucketService;
        private readonly UserManager<User> _userManager;
        private readonly ProductService _productService;
        public BucketController(BucketService bucketService, UserManager<User> userManager, ProductService productService)
        {
            _bucketService = bucketService;
            _userManager = userManager;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity!.Name!).Result;
            var bucket = await _bucketService.GetBucketAsync(user!.Id);
            if (bucket == null)
            {
                await _bucketService.AddBucketToUser(user.Id);
                bucket = await _bucketService.GetBucketAsync(user.Id);
            }
            return View(bucket);
        }
        public async Task<IActionResult> AddToBucket(Guid productId, int quantity = 1)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null) return Unauthorized();

            await _bucketService.AddProductToBucket(user.Id, productId, quantity);
            return RedirectToAction("Index");
        }
    }
}
