using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetMenuAsync(int clientId);
        Task<MenuItem?> GetByIdAsync(int id);
        Task<MenuItem> CreateAsync(MenuItem model);
        Task<bool> UpdateAsync(int id, MenuItem model);
        Task<bool> DeleteAsync(int id);
    }
}
