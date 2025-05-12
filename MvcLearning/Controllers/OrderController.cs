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
        private readonly ShopService _shopService;
        public OrderController(OrderService orderService, ShopService shopService)
        {
            _orderService = orderService;
            _shopService = shopService;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _orderService.GetOrderByIdAsync(orderId, token);
            if (order == null)
                return NotFound();

            // Попытка получить магазин (если это продавец)
            var shop = await _shopService.GetShopAsync(userId!);
            Guid? shopId = shop?.Id;

            bool isCustomer = order.UserId == userId;
            bool isShopOwner = shopId.HasValue && order.OrderItems.Any(oi => oi.ShopId == shopId.Value);

            if (!isCustomer && !isShopOwner)
                return NotFound();

            if (isShopOwner && !isCustomer)
            {
                order.OrderItems = order.OrderItems
                    .Where(oi => oi.ShopId == shopId.Value)
                    .ToList();
            }

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
