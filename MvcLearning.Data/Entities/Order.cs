using MvcLearning.Data.Enums;

namespace MvcLearning.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public List<Product>? OrderItems { get; set; }
        public DateTime OrderingTime { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}