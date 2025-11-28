using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetByClientAsync(int clientId);
    }
}
