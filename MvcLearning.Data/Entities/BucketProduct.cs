using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcLearning.Data.Entities
{
    public class BucketProduct
    {
        public Guid Id { get; set; }
        public Guid BucketId { get; set; }
        public Bucket? Bucket { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
