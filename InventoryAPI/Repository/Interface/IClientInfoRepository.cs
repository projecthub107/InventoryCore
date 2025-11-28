using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IClientInfoRepository : IGenericRepository<ClientInfo>
    {
        Task<IEnumerable<ClientInfo>> GetActiveAsync();
        Task<ClientInfo?> GetByNameAsync(string name);
    }
}
