using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<IEnumerable<Location>> GetByClientAsync(int clientId);
    }
}
