using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepo;

        public RoleService(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public Task<IEnumerable<Role>> GetByClientAsync(int clientId) =>
            _roleRepo.GetByClientAsync(clientId);

        public Task<Role?> GetByIdAsync(int id) =>
            _roleRepo.GetByIdAsync(id);

        public async Task<Role> CreateAsync(Role model)
        {
            model.CreatedDate = DateTime.UtcNow;
            model.ModifiedDate = model.CreatedDate;

            await _roleRepo.AddAsync(model);
            await _roleRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, Role model)
        {
            var existing = await _roleRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = model.Name;
            existing.Status = model.Status;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _roleRepo.Update(existing);
            await _roleRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _roleRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _roleRepo.Remove(existing);
            await _roleRepo.SaveChangesAsync();
            return true;
        }
    }
}
