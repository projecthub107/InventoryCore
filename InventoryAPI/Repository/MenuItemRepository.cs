using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MenuItem>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(m => m.ClientId == clientId && (m.Status == 1 || m.Status == null))
                .OrderBy(m => m.ParentId)
                .ThenBy(m => m.Serial)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuTreeAsync(int clientId)
        {
            // Same as GetByClientAsync for now; you can extend to build a tree DTO
            return await GetByClientAsync(clientId);
        }
    }
}
