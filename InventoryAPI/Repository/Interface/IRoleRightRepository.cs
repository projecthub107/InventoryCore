using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IRoleRightRepository : IGenericRepository<RoleRight>
    {
        Task<IEnumerable<RoleRight>> GetByRoleAsync(int clientId, int roleId);
    }
}
