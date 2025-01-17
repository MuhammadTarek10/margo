using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet properties for your entities
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    public DbSet<Cart> Carts { get; set; } = default!;
    public DbSet<CartItem> CartItems { get; set; } = default!;
    public DbSet<Notification> Notifications { get; set; } = default!;
    public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
    public DbSet<Analytics> Analytics { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure the User entity (if needed)
        builder.Entity<User>(entity =>
        {
            entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Address).HasMaxLength(200);
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.UpdatedAt).IsRequired();
        });

        // Configure the Product entity
        builder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(p => p.Stock).IsRequired();
            entity.Property(p => p.Category).HasMaxLength(50);
            entity.Property(p => p.CreatedAt).IsRequired();
            entity.Property(p => p.UpdatedAt).IsRequired();
        });

        // Configure the Cart entity
        builder.Entity<Cart>(entity =>
        {
            entity.HasOne(c => c.User)
                  .WithMany(u => u.Carts)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(c => c.CreatedAt).IsRequired();
            entity.Property(c => c.UpdatedAt).IsRequired();
        });

        // Configure the CartItem entity
        builder.Entity<CartItem>(entity =>
        {
            entity.HasOne(ci => ci.Cart)
                  .WithMany(c => c.CartItems)
                  .HasForeignKey(ci => ci.CartId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ci => ci.Product)
                  .WithMany(p => p.CartItems)
                  .HasForeignKey(ci => ci.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(ci => ci.Quantity).IsRequired();
        });

        // Configure the Order entity
        builder.Entity<Order>(entity =>
        {
            entity.HasOne(o => o.User)
                  .WithMany(u => u.Orders)
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(o => o.Status).HasConversion<string>().IsRequired(); // Store enum as string
            entity.Property(o => o.PaymentIntentId).HasMaxLength(100);
            entity.Property(o => o.CreatedAt).IsRequired();
            entity.Property(o => o.UpdatedAt).IsRequired();
        });

        // Configure the OrderItem entity
        builder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(oi => oi.Order)
                  .WithMany(o => o.Items)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Product)
                  .WithMany(p => p.OrderItems)
                  .HasForeignKey(oi => oi.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(oi => oi.Quantity).IsRequired();
            entity.Property(oi => oi.PriceAtTimeOfOrder).HasColumnType("decimal(18,2)").IsRequired();
        });

        // Configure the Notification entity
        builder.Entity<Notification>(entity =>
        {
            entity.HasOne(n => n.User)
                  .WithMany(u => u.Notifications)
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(n => n.Message).HasMaxLength(500).IsRequired();
            entity.Property(n => n.IsRead).IsRequired();
            entity.Property(n => n.CreatedAt).IsRequired();
        });

        // Configure the ChatMessage entity
        builder.Entity<ChatMessage>(entity =>
        {
            entity.HasOne(cm => cm.User)
                  .WithMany(u => u.SentMessages)
                  .HasForeignKey(cm => cm.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cm => cm.Agent)
                  .WithMany(u => u.ReceivedMessages)
                  .HasForeignKey(cm => cm.AgentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(cm => cm.Message).HasMaxLength(1000).IsRequired();
            entity.Property(cm => cm.IsFromUser).IsRequired();
            entity.Property(cm => cm.CreatedAt).IsRequired();
        });

        // Configure the Analytics entity
        builder.Entity<Analytics>(entity =>
        {
            entity.Property(a => a.TotalSales).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(a => a.TotalOrders).IsRequired();
            entity.Property(a => a.TotalUsers).IsRequired();
            entity.Property(a => a.Date).IsRequired();

            entity.HasOne(a => a.MostSoldProduct)
                  .WithMany()
                  .HasForeignKey(a => a.MostSoldProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}