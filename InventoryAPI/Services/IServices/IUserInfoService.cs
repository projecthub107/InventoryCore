using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IUserInfoService
    {
        Task<UserInfo?> GetByIdAsync(int id);
        Task<UserInfo?> GetByUserNameAsync(string userName, int clientId);
        Task<IEnumerable<UserInfo>> GetByClientAsync(int clientId);
        Task<UserInfo> CreateAsync(UserInfo model);
        Task<bool> UpdateAsync(int id, UserInfo model);
        Task<bool> DeleteAsync(int id);
    }
}
