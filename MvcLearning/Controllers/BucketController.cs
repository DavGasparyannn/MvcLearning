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
        public BucketController(BucketService bucketService, UserManager<User> userManager)
        {
            _bucketService = bucketService;
            _userManager = userManager;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantityExact([FromBody] QuantityUpdateDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _bucketService.SetProductQuantityAsync(user.Id, dto.ProductId, dto.Quantity);
            if (!result.Success) return BadRequest(result.Message);

            return Json(new
            {
                quantity = result.NewQuantity,
                subtotal = result.Subtotal.ToString("C"),
                total = result.Total.ToString("C")
            });
        }
    }
}
