using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities.Images;

namespace MvcLearning.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProductImage>? Images { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Bucket>? Buckets { get; set; }
        public List<OrderItem> OrderItems { get; set; }


    }
}
