using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetByClientAsync(int clientId);
        Task<Location?> GetByIdAsync(int id);
        Task<Location> CreateAsync(Location model);
        Task<bool> UpdateAsync(int id, Location model);
        Task<bool> DeleteAsync(int id);
    }
}
