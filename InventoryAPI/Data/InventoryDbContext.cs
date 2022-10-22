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
        public DbSet<InventoryAPI.Models.Country> Country { get; set; }
        public DbSet<InventoryAPI.Models.History> History { get; set; }
        public DbSet<InventoryAPI.Models.Location> Location { get; set; }
        public DbSet<InventoryAPI.Models.Manufacturer> Manufacturer { get; set; }
        public DbSet<InventoryAPI.Models.MenuItem> MenuItem { get; set; }
        public DbSet<InventoryAPI.Models.Preferences> Preferences { get; set; }
        public DbSet<InventoryAPI.Models.ProductTransaction> ProductTransaction { get; set; }
        public DbSet<InventoryAPI.Models.Questions> Questions { get; set; }
        public DbSet<InventoryAPI.Models.Role> Role { get; set; }
        public DbSet<InventoryAPI.Models.RoleRight> RoleRight { get; set; }
        public DbSet<InventoryAPI.Models.TransactionType> TransactionType { get; set; }
        public DbSet<InventoryAPI.Models.Unit> Unit { get; set; }


    }
}
