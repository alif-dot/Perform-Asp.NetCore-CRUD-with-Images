using CRUDwithImage.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDwithImage.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}
