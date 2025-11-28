using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IPreferencesService
    {
        Task<Preferences?> GetByClientAsync(int clientId);
        Task<Preferences> SaveOrUpdateAsync(Preferences model);
    }
}
