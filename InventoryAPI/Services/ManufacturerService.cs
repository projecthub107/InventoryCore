using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepo;

        public ManufacturerService(IManufacturerRepository manufacturerRepo)
        {
            _manufacturerRepo = manufacturerRepo;
        }

        public Task<IEnumerable<Manufacturer>> GetByClientAsync(int clientId) =>
            _manufacturerRepo.GetByClientAsync(clientId);

        public Task<Manufacturer?> GetByIdAsync(int id) =>
            _manufacturerRepo.GetByIdAsync(id);

        public async Task<Manufacturer> CreateAsync(Manufacturer model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.ModifiedDate = now;

            await _manufacturerRepo.AddAsync(model);
            await _manufacturerRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, Manufacturer model)
        {
            var existing = await _manufacturerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.ManufacturerName = model.ManufacturerName;
            existing.Address = model.Address;
            existing.City = model.City;
            existing.State = model.State;
            existing.Zip = model.Zip;
            existing.Status = model.Status;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _manufacturerRepo.Update(existing);
            await _manufacturerRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _manufacturerRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _manufacturerRepo.Remove(existing);
            await _manufacturerRepo.SaveChangesAsync();
            return true;
        }
    }
}
