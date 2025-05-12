public class OrderItemDTO
{
    public Guid ShopId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchaseTime { get; set; }
}