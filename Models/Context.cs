using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mvc_Project.Models
{
    public class Context: IdentityDbContext<User, IdentityRole<int>,int>
    {
        public Context() : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
      
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductTagMapping> ProductTagMappings { get; set; }
        public DbSet<SellerProduct> SellerProducts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderCoupon> OrderCoupons { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ProductQuestion> ProductQuestions { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

       

    }
}
