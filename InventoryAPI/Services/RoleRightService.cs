using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class RoleRightService : IRoleRightService
    {
        private readonly IRoleRightRepository _roleRightRepo;

        public RoleRightService(IRoleRightRepository roleRightRepo)
        {
            _roleRightRepo = roleRightRepo;
        }

        public Task<IEnumerable<RoleRight>> GetByRoleAsync(int clientId, int roleId) =>
            _roleRightRepo.GetByRoleAsync(clientId, roleId);

        public async Task<RoleRight> AddAsync(RoleRight model)
        {
            await _roleRightRepo.AddAsync(model);
            await _roleRightRepo.SaveChangesAsync();
            return model;
        }
    }
}
