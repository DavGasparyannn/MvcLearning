using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MvcLearning.Data.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? RegistrationIpAddress { get; set; }
        public string? LastLoginIpAddress { get; set; }
        public Bucket Bucket { get; set; }
        public Shop Shop { get; set; }
        public IEnumerable<Order>? Orders { get; set; }

    }
}
