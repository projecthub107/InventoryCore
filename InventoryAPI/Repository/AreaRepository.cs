using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        public AreaRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Area>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(a => a.ClientId == clientId && (a.Status == 1 || a.Status == null))
                .ToListAsync();
        }
    }
}
