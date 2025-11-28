using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IHistoryService
    {
        Task<IEnumerable<History>> GetByClientAsync(int clientId);
        Task<IEnumerable<History>> GetByProductAsync(int clientId, int productId);
    }
}
