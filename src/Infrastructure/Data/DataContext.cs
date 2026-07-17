using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Courier> Couriers => Set<Courier>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Restaurant
    modelBuilder.Entity<Restaurant>(entity =>
    {
        entity.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(x => x.ContactPhone)
            .HasMaxLength(20);

        entity.Property(x => x.Description)
            .HasMaxLength(500);

        entity.Property(x => x.Rating);

        entity.HasMany(x => x.MenuItems)
            .WithOne(x => x.Restaurant)
            .HasForeignKey(x => x.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(x => x.Orders)
            .WithOne(x => x.Restaurant)
            .HasForeignKey(x => x.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);
    });

    modelBuilder.Entity<Menu>(entity =>
    {
        entity.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(x => x.Price)
            .HasPrecision(10, 2);

        entity.HasMany(x => x.OrderDetails)
            .WithOne(x => x.MenuItem)
            .HasForeignKey(x => x.MenuItemId);
    });

    // User
    modelBuilder.Entity<User>(entity =>
    {
        entity.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        entity.Property(x => x.Phone)
            .HasMaxLength(20);

        entity.HasMany(x => x.Orders)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        entity.HasOne(x => x.CourierProfile)
            .WithOne(x => x.User)
            .HasForeignKey<Courier>(x => x.UserId);
    });

    // Order
    modelBuilder.Entity<Order>(entity =>
    {
        entity.Property(x => x.TotalAmount)
            .HasPrecision(10, 2);

        entity.HasMany(x => x.OrderDetails)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // OrderDetail
    modelBuilder.Entity<OrderDetail>(entity =>
    {
        entity.Property(x => x.Price)
            .HasPrecision(10, 2);
    });

    // Courier
    modelBuilder.Entity<Courier>(entity =>
    {
        entity.Property(x => x.CurrentLocation)
            .HasMaxLength(200);
        
        entity.HasMany(x => x.Orders)
            .WithOne(x => x.Courier)
            .HasForeignKey(x => x.CourierId);
    });
}
}