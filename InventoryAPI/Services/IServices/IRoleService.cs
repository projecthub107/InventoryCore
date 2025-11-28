using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetByClientAsync(int clientId);
        Task<Role?> GetByIdAsync(int id);
        Task<Role> CreateAsync(Role model);
        Task<bool> UpdateAsync(int id, Role model);
        Task<bool> DeleteAsync(int id);
    }
}
