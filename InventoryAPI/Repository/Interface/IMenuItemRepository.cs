using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetByClientAsync(int clientId);
        Task<IEnumerable<MenuItem>> GetMenuTreeAsync(int clientId);
    }
}
