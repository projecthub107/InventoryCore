using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class ProductTransactionRepository
        : GenericRepository<ProductTransaction>, IProductTransactionRepository
    {
        public ProductTransactionRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductTransaction>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(t => t.ClientId == clientId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductTransaction>> GetByProductAsync(int clientId, int productId)
        {
            return await _dbSet
                .Where(t => t.ClientId == clientId && t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}
