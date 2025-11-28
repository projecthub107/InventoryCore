using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetByClientAsync(int clientId);
        Task<Manufacturer?> GetByIdAsync(int id);
        Task<Manufacturer> CreateAsync(Manufacturer model);
        Task<bool> UpdateAsync(int id, Manufacturer model);
        Task<bool> DeleteAsync(int id);
    }
}
