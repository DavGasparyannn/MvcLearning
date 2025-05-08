namespace MvcLearning.Business
{
    public class ShopCreatingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}