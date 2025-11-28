using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {
        public HistoryRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<History>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(h => h.ClientId == clientId)
                .OrderByDescending(h => h.TransactionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<History>> GetByProductAsync(int clientId, int productId)
        {
            return await _dbSet
                .Where(h => h.ClientId == clientId && h.ProductId == productId)
                .OrderByDescending(h => h.TransactionDate)
                .ToListAsync();
        }
    }
}
