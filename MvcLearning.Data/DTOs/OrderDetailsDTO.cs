using MvcLearning.Data.Enums;

public class OrderDetailsDTO
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderingTime { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}
