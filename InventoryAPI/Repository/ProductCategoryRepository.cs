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
    public class ProductCategoryRepository
        : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(InventoryDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProductCategory>> GetActiveByClientAsync(int clientId)
        {
            // Assuming Status = 1 means active
            return await _dbSet
                .Where(c => c.ClientId == clientId && (c.Status == 1 || c.Status == null))
                .ToListAsync();
        }
    }
}
