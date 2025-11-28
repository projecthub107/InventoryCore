using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class ClientInfoRepository : GenericRepository<ClientInfo>, IClientInfoRepository
    {
        public ClientInfoRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ClientInfo>> GetActiveAsync()
        {
            return await _dbSet
                .Where(c => c.Status == 1 || c.Status == null)
                .ToListAsync();
        }

        public async Task<ClientInfo?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
