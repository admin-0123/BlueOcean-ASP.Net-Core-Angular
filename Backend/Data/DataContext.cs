using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Virta.Entities;

namespace Virta.Data
{
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User
            builder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.SetNull);

            // Category
            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<Category>()
                .HasIndex(c => c.Title)
                .IsUnique();

            // Product
            builder.Entity<Product>()
                .HasIndex(p => p.Title)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasDefaultValue(0);

            builder.Entity<Product>()
                .Property(p => p.Type)
                .HasDefaultValue(Product.ProductTypes.Simple);

            builder.Entity<Product>()
                .Property(p => p.Visible)
                .HasDefaultValue(Product.ProductVisibility.Invisible);

            builder.Entity<Product>()
                .Property(p => p.Active)
                .HasDefaultValue(false);

            builder.Entity<Product>()
                .HasMany(p => p.AssociatedProducts)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            // Product Attributes
            builder.Entity<ProductAttributes>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Attributes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductAttributes>()
                .HasIndex(p => p.Name);

            builder.Entity<ProductAttributes>()
                .Property(pa => pa.Priority)
                .HasDefaultValue(0);

            builder.Entity<ProductAttributes>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsConcurrencyToken();

            // Order
            builder.Entity<Order>()
                .Property(o => o.ShippingCost)
                .HasColumnType("numeric(8,2)")
                .HasDefaultValue(0);

            builder.Entity<Order>()
                .Property(o => o.Status)
                .HasDefaultValue("Pending");

            builder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Order>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsConcurrencyToken();

            // Order Product
            builder.Entity<OrderProduct>()
                .HasKey(op =>
                    new { op.OrderId, op.ProductId }
                );

            builder.Entity<OrderProduct>()
                .Property(op => op.Price)
                .HasColumnType("numeric(9,2)")
                .HasDefaultValue(0);

            builder.Entity<OrderProduct>()
                .Property(op => op.Quantity)
                .HasDefaultValue(0);

            // Addresses
            builder.Entity<Address>()
                .Property(A => A.Country)
                .HasDefaultValue("Georgia");

            builder.Entity<Address>()
                .Property(A => A.Primary)
                .HasDefaultValue(false);
        }
    }
}
