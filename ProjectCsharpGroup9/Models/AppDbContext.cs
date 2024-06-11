using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProjectCsharpGroup9.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() // ctor ko tham số
        {

        }

        public AppDbContext(DbContextOptions options) : base(options) // ctor của lớp đc kế thừa
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }  
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartsDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Gallery> Gallerys { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//cấu hình liên kết đến SQL
        {
            //optionsBuilder.UseSqlServer("Server=NIHONGGOO\\SQLEXPRESS;Database =ProjectCsharpGroup9; Trusted_Connection = True; TrustServerCertificate = True");
            optionsBuilder.UseSqlServer("Server=MSI;Database =ProjectCsharpGroup9; Trusted_Connection = True; TrustServerCertificate = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//Cấu hình cho các đối tượng trong bảng CSDL
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //base.OnModelCreating(modelBuilder);
        }
    }
}
