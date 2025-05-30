﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcLearning.Business.Models.Shop;
using MvcLearning.Business.Services;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Enums;

namespace MvcLearning.Controllers
{
    [Authorize(Roles = "ShopOwner")]
    public class ShopController : Controller
    {
        private readonly ShopService _shopService;
        private readonly OrderService _orderService;
        private readonly UserManager<User> _userManager;
        public ShopController(ShopService shopService, OrderService orderService, UserManager<User> userManager)
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
        [AllowAnonymous]
        public async Task<IActionResult> Info(Guid shopId)
        {
            var shop = await _shopService.GetShopAsync(shopId);
            if (shop == null)
                return NotFound();

            return View(shop);
        }
        public async Task<IActionResult> Orders(CancellationToken token)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shop = await _shopService.GetShopAsync(userId!); // сам напиши этот метод, если его нет
            if (shop == null) return NotFound();
            var orders = await _orderService.GetOrdersByShopIdAsync(shop.Id, token);
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
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, OrderStatus newStatus, CancellationToken token)
        {
            var shopOwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var success = await _shopService.UpdateOrderStatus(orderId, newStatus, shopOwnerId!, token);

            if (!success)
                return BadRequest();

            return Ok();
        }
        public async Task<IActionResult> Customers(CancellationToken token = default)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shop = await _shopService.GetShopAsync(userId!);
            if (shop == null) return NotFound();
            var customers = await _shopService.GetCustomers(shop.Id, token);
            return View(customers);
        }
        public async Task<IActionResult> Customer(string userId, CancellationToken token = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var orders = await _orderService.GetOrdersByUserIdAsync(userId, token);
            return View(orders);
        }
    }
}
