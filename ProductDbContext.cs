using Microsoft.EntityFrameworkCore;

namespace Xmas_tree_Json
{
    public class ProductDbContext : DbContext
    {
        public DbSet<ProductDB> ProductDBList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"data source=DESKTOP-AP2F245\SQLEXPRESS01;Initial Catalog=TestDB;Integrated Security=True;
                Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
        }

    }
}
