using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepo;

        public LocationService(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        public Task<IEnumerable<Location>> GetByClientAsync(int clientId) =>
            _locationRepo.GetByClientAsync(clientId);

        public Task<Location?> GetByIdAsync(int id) =>
            _locationRepo.GetByIdAsync(id);

        public async Task<Location> CreateAsync(Location model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.ModifiedDate = now;

            await _locationRepo.AddAsync(model);
            await _locationRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, Location model)
        {
            var existing = await _locationRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.LocationName = model.LocationName;
            existing.Address = model.Address;
            existing.City = model.City;
            existing.State = model.State;
            existing.Zip = model.Zip;
            existing.Status = model.Status;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _locationRepo.Update(existing);
            await _locationRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _locationRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _locationRepo.Remove(existing);
            await _locationRepo.SaveChangesAsync();
            return true;
        }
    }
}
