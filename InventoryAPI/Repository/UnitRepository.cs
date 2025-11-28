using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Unit>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(u => u.ClientId == clientId && (u.Status == 1 || u.Status == null))
                .ToListAsync();
        }
    }
}
