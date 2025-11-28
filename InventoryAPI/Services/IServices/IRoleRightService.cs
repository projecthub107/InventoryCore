using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IRoleRightService
    {
        Task<IEnumerable<RoleRight>> GetByRoleAsync(int clientId, int roleId);
        Task<RoleRight> AddAsync(RoleRight model);
    }
}
