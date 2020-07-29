using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCeTicaretRasim.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
            Database.Connection.ConnectionString = @"server=(localdb)\mssqllocaldb; database= MVCeTicaretRasim; Trusted_Connection = true";
        }

        public DbSet<AdminEmployee> AdminEmployees { get; set; }
        public DbSet<AdminLogin> AdminLogins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RecentlyView> RecentlyViews { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles1 { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Wishlist> Whislists { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}