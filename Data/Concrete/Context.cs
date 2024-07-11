using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class Context:IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS; database=baskidabaski2; integrated security=true");

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
       // public DbSet<ProductCategory> ProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {base.OnModelCreating(builder);
            builder.Entity<ProductCategory>().HasKey(t => new { t.ProductId, t.CategoryId });
            builder.Entity<ProductCategory>().HasOne(pc=>pc.Product).WithMany(p=>p.ProductCategories).HasForeignKey(pc=>pc.ProductId);
            builder.Entity<ProductCategory>().HasOne(pc => pc.Category).WithMany(p => p.ProductCategories).HasForeignKey(pc => pc.CategoryId);

            
        }
    }
}
