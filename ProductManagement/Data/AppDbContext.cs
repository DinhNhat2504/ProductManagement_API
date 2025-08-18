using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System;
using System.Data;

namespace ProductManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ChatRoom> Chats { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ ChatRoom - User (khách hàng)
            modelBuilder.Entity<ChatRoom>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.ChatRoomsAsUser)
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình mối quan hệ ChatRoom - Admin
            modelBuilder.Entity<ChatRoom>()
                .HasOne(cr => cr.Admin)
                .WithMany(u => u.ChatRoomsAsAdmin)
                .HasForeignKey(cr => cr.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
