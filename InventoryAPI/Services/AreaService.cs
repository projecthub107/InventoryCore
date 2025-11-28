using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepo;

        public AreaService(IAreaRepository areaRepo)
        {
            _areaRepo = areaRepo;
        }

        public Task<IEnumerable<Area>> GetAreasAsync(int clientId) =>
            _areaRepo.GetByClientAsync(clientId);

        public Task<Area?> GetByIdAsync(int id) =>
            _areaRepo.GetByIdAsync(id);

        public async Task<Area> CreateAsync(Area model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.ModifiedDate = now;

            await _areaRepo.AddAsync(model);
            await _areaRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, Area model)
        {
            var existing = await _areaRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.AreaName = model.AreaName;
            existing.Status = model.Status;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _areaRepo.Update(existing);
            await _areaRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _areaRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _areaRepo.Remove(existing);
            await _areaRepo.SaveChangesAsync();
            return true;
        }
    }
}
