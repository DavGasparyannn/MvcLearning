using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business.Services;
using MvcLearning.Data.Entities;
using NuGet.Common;

namespace MvcLearning.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CancellationToken token = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            try
            {
                var order = await _orderService.CreateOrderFromUserBucketAsync(userId, token);
                return RedirectToAction("Details", new { orderId = order.Id });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Bucket");
            }
        }
        public async Task<IActionResult> Details(Guid orderId, CancellationToken token = default)
        {

            var order = await _orderService.GetOrderByIdAsync(orderId, token);
            if (order == null)
                return NotFound();
            return View(order);
        }
        public async Task<IActionResult> MyOrders(CancellationToken token = default) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var orders = await _orderService.GetOrdersByUserIdAsync(userId, token);
            return View(orders);
        }
    }
}
