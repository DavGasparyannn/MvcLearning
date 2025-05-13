using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Enums;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderFromBucket(Bucket bucket, CancellationToken token)
        {
            if (bucket == null || bucket.BucketProducts == null || !bucket.BucketProducts.Any())
                throw new Exception("Bucket is empty or not found");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == bucket.UserId, token);
            if (user == null)
                throw new Exception("User not found");

            var order = new Order
            {
                User = user,
                Status = OrderStatus.Pending,

                OrderingTime = DateTime.UtcNow,
                TotalAmount = bucket.BucketProducts.Sum(bp => bp.Quantity * bp.Product.Price),
                OrderItems = new List<OrderItem>()

            };
            var shopsHandled = new HashSet<Guid>();

            foreach (var bp in bucket.BucketProducts)
            {
                var product = await _context.Products
    .Include(p => p.Shop)
        .ThenInclude(s => s.Customers)
    .FirstOrDefaultAsync(p => p.Id == bp.Product.Id, token);

                if (product == null)
                    throw new Exception($"Product with ID {bp.Product.Id} not found");

                order.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    Order = order,
                    ProductId = product.Id,
                    Quantity = bp.Quantity,
                    PriceAtPurchaseTime = product.Price
                });
                if (!shopsHandled.Contains(product.ShopId))
                {

                    if (!bp.Product.Shop.Customers.Any(u => order.UserId == u.Id))
                    {
                        await AddCustomerToShop(bp.Product.ShopId, order.UserId, token); // Add customer to shop
                    }
                    shopsHandled.Add(product.ShopId);
                }
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(token);
            return order;

        }

        public async Task<OrderDetailsDTO?> GetOrderByIdAsync(Guid orderId, CancellationToken token)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDetailsDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    Status = o.Status,
                    OrderingTime = o.OrderingTime,
                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtPurchaseTime),
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        PriceAtPurchaseTime = oi.PriceAtPurchaseTime,
                        ShopId = oi.Product.ShopId
                    }).ToList()
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task<List<OrderDetailsDTO>?> GetOrdersByUserIdAsync(string userId, CancellationToken token)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .Select(o => new OrderDetailsDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    Status = o.Status,
                    OrderingTime = o.OrderingTime,
                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtPurchaseTime),
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        PriceAtPurchaseTime = oi.PriceAtPurchaseTime
                    }).ToList()
                })
                .ToListAsync(token);
        }
        private async Task AddCustomerToShop(Guid shopId, string userId, CancellationToken token)
        {
            var shop = await _context.Shops
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(s => s.Id == shopId, token);
            if (shop == null)
                throw new Exception("Shop not found");
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId, token);
            if (user == null)
                throw new Exception("User not found");

                shop.Customers.Add(user);

            await _context.SaveChangesAsync(token);
        }
    }
}
