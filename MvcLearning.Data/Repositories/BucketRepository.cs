using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcLearning.Data.Entities;
using MvcLearning.Data.Interfaces;

namespace MvcLearning.Data.Repositories
{
    public class BucketRepository : IBucketRepository
    {
        private readonly ApplicationDbContext _context;
        public BucketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddBucketToUser(string userId, CancellationToken token)
        {
            var bucket = new Bucket
            {
                UserId = userId
            };
            await _context.Buckets.AddAsync(bucket, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Bucket> GetBucketByUserId(string userId, CancellationToken token)
        {
            var bucket = await _context.Buckets
                .Include(b => b.BucketProducts)
                    .ThenInclude(bp => bp.Product)
                        .ThenInclude(p => p.Shop)
                .FirstOrDefaultAsync(b => b.UserId == userId, token);

     

            return bucket;
        }
        public async Task UpdateBucketAsync(Bucket bucket, CancellationToken token)
        {
             _context.Buckets.Update(bucket);
            await _context.SaveChangesAsync(token);
        }
    }
    
}
