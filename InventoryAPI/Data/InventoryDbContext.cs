using InventoryAPI.Models.SP;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;

namespace InventoryAPI.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext()
        {
        }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }
        public DbSet<SP_UserInfo> SP_UserInfo { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<InventoryAPI.Models.SP.SP_Product> SP_Product { get; set; }
        public DbSet<InventoryAPI.Models.Area> Area { get; set; }
        public DbSet<InventoryAPI.Models.ClientInfo> ClientInfo { get; set; }


    }
}
