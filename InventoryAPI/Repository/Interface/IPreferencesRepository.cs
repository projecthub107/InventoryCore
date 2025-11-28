using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IPreferencesRepository : IGenericRepository<Preferences>
    {
        Task<Preferences?> GetByClientAsync(int clientId);
    }
}
