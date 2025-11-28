using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Models;

namespace InventoryAPI.Repository
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(InventoryDbContext context) : base(context)
        {
        }

        public Task AddAsync(Location entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(l => l.ClientId == clientId && (l.Status == 1 || l.Status == null))
                .ToListAsync();
        }

        public void Remove(Location entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Location entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Location>> IGenericRepository<Location>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Location>> ILocationRepository.GetByClientAsync(int clientId)
        {
            throw new NotImplementedException();
        }

        Task<Location?> IGenericRepository<Location>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
