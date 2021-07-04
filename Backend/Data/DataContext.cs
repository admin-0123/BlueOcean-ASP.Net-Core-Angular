using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Virta.Entities;

namespace Virta.Data
{
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
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

            builder.Entity<Product>()
                .HasMany(p => p.ProductAttributes)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Entity<Product>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .IsConcurrencyToken();


            // Product Attributes
            builder.Entity<ProductAttribute>()
                .Property(pa => pa.Priority)
                .HasDefaultValue(0);

            builder.Entity<ProductAttribute>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .IsConcurrencyToken();



            // Product Images
            builder.Entity<ProductImage>()
                .Property(pi => pi.Primary)
                .HasDefaultValue(false);

            builder.Entity<ProductImage>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .IsConcurrencyToken();

            // Attributes
            builder.Entity<Attribute>()
                .HasIndex(a => a.Name)
                .IsUnique();

            builder.Entity<Attribute>()
                .HasIndex(a => a.Title)
                .IsUnique();

            builder.Entity<Attribute>()
                .HasMany(a => a.ProductAttributes)
                .WithOne(a => a.Attribute)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Attribute>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Entity<Attribute>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
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
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Entity<Order>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
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
                .Property(a => a.Country)
                .HasDefaultValue("Georgia");

            builder.Entity<Address>()
                .Property(a => a.Primary)
                .HasDefaultValue(false);

            builder.Entity<Address>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Entity<Address>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .IsConcurrencyToken();
        }
    }
}
