using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IHistoryRepository : IGenericRepository<History>
    {
        Task<IEnumerable<History>> GetByClientAsync(int clientId);
        Task<IEnumerable<History>> GetByProductAsync(int clientId, int productId);
    }
}
