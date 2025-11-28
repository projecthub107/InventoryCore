using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class PreferencesRepository : GenericRepository<Preferences>, IPreferencesRepository
    {
        public PreferencesRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<Preferences?> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.ClientId == clientId);
        }
    }
}
