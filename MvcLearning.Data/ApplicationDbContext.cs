using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcLearning.Data.Entities;

namespace MvcLearning.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BucketProduct> BucketProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User — Bucket (1 к 1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Bucket)
                .WithOne(b => b.User)
                .HasForeignKey<Bucket>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User — Orders (1 ко многим)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Cascade);

            // BucketProduct (Many-to-Many между Bucket и Product)
            modelBuilder.Entity<BucketProduct>()
                .HasKey(bp => bp.Id);

            modelBuilder.Entity<BucketProduct>()
                .HasAlternateKey(bp => new { bp.BucketId, bp.ProductId });

            modelBuilder.Entity<BucketProduct>()
                .HasOne(bp => bp.Bucket)
                .WithMany(b => b.BucketProducts)
                .HasForeignKey(bp => bp.BucketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BucketProduct>()
                .HasOne(bp => bp.Product)
                .WithMany()
                .HasForeignKey(bp => bp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product — Category (Many-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));

            // Order — OrderItems (1 ко многим)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product — OrderItems (1 ко многим)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Первичный ключ
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            // User — Shop (1 к 1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Shop)
                .WithOne(s => s.Owner)
                .HasForeignKey<Shop>(s => s.OwnerId);

            // Shop — Products (1 ко многим)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Shop)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shop — Customers (Many-to-Many)
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Customers)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "ShopCustomers",
                    j => j.HasOne<User>().WithMany().HasForeignKey("CustomersId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Shop>().WithMany().HasForeignKey("ShopId").OnDelete(DeleteBehavior.NoAction));

            // Seed data for roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "ShopOwner",
                    NormalizedName = "SHOPOWNER"
                });

            // Автогенерация ID
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Bucket>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedOnAdd();
        }
    }
}
