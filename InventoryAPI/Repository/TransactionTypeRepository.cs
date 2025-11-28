using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class TransactionTypeRepository
        : GenericRepository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TransactionType>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(t => t.ClientId == clientId && (t.Status == 1 || t.Status == null))
                .ToListAsync();
        }
    }
}
