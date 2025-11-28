using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class RoleRightRepository : GenericRepository<RoleRight>, IRoleRightRepository
    {
        public RoleRightRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoleRight>> GetByRoleAsync(int clientId, int roleId)
        {
            return await _dbSet
                .Where(rr => rr.ClientId == clientId && rr.RoleId == roleId)
                .ToListAsync();
        }
    }
}
