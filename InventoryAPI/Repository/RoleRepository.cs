using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Role>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(r => r.ClientId == clientId && (r.Status == 1 || r.Status == null))
                .ToListAsync();
        }
    }
}
