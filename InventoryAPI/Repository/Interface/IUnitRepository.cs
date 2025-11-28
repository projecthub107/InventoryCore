using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IUnitRepository : IGenericRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetByClientAsync(int clientId);
    }
}
