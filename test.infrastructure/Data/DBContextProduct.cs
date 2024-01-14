using Microsoft.EntityFrameworkCore;
using test.domain.Entities;

namespace test.infrastructure.Data;
    public class DBContextProduct : DbContext
    {
        public DBContextProduct(DbContextOptions<DBContextProduct> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        
    }
  