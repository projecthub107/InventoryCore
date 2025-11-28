using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IManufacturerRepository : IGenericRepository<Manufacturer>
    {
        Task<IEnumerable<Manufacturer>> GetByClientAsync(int clientId);
    }
}
