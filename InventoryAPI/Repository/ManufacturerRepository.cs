using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Manufacturer>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(m => m.ClientId == clientId && (m.Status == 1 || m.Status == null))
                .ToListAsync();
        }
    }
}
