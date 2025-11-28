using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IUserInfoRepository : IGenericRepository<UserInfo>
    {
        Task<UserInfo?> GetByUserNameAsync(string userName, int clientId);
        Task<IEnumerable<UserInfo>> GetByClientAsync(int clientId);
    }
}
