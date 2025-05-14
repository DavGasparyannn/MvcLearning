using MvcLearning.Data.Enums;

namespace MvcLearning.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public string UserId { get; set; }
        public DateTime OrderingTime { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
	}
}