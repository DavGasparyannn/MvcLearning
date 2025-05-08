using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcLearning.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public IEnumerable<string>? ImageUrls { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<Category>? Categories { get; set; }



    }
}
