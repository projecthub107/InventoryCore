using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IEnumerable<Role>> GetByClientAsync(int clientId);
    }
}
