using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        Task<IEnumerable<Area>> GetByClientAsync(int clientId);
    }
}
