using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcLearning.Data.Entities;

namespace MvcLearning.Data.Interfaces
{
    public interface IBucketRepository
    {
        Task AddBucketToUser(string userId, CancellationToken token);
        Task<Bucket> GetBucketByUserId(string userId, CancellationToken token);
        Task UpdateBucketAsync(Bucket bucket, CancellationToken token);
    }
}
