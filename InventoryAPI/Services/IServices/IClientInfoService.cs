using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IClientInfoService
    {
        Task<IEnumerable<ClientInfo>> GetAllAsync();
        Task<IEnumerable<ClientInfo>> GetActiveAsync();
        Task<ClientInfo?> GetByIdAsync(int id);
        Task<ClientInfo?> GetByNameAsync(string name);
        Task<ClientInfo> CreateAsync(ClientInfo model);
        Task<bool> UpdateAsync(int id, ClientInfo model);
        Task<bool> DeleteAsync(int id);
    }
}
