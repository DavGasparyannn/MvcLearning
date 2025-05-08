namespace MvcLearning.Data.Entities
{
    public class Bucket
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public IEnumerable<Product>? Products { get; set; }

    }
}