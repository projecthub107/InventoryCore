using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync(int? clientId = null)
        {
            var query = _dbSet
                .Include(p => p.CategoryId)
                .AsQueryable();

            if (clientId.HasValue)
            {
                query = query.Where(p => p.ClientId == clientId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
