using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public interface IUserInfoRepository : IGenericRepository<UserInfo>
    {
        Task<UserInfo?> GetByUserNameAsync(string userName, int clientId);
        Task<IEnumerable<UserInfo>> GetByClientAsync(int clientId);
    }
}
