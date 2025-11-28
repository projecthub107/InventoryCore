using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class ClientInfoService : IClientInfoService
    {
        private readonly IClientInfoRepository _clientRepo;

        public ClientInfoService(IClientInfoRepository clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public Task<IEnumerable<ClientInfo>> GetAllAsync() => _clientRepo.GetAllAsync();

        public Task<IEnumerable<ClientInfo>> GetActiveAsync() => _clientRepo.GetActiveAsync();

        public Task<ClientInfo?> GetByIdAsync(int id) => _clientRepo.GetByIdAsync(id);

        public Task<ClientInfo?> GetByNameAsync(string name) => _clientRepo.GetByNameAsync(name);

        public async Task<ClientInfo> CreateAsync(ClientInfo model)
        {
            model.CreatedDate = DateTime.UtcNow;
            model.ModifiedDate = model.CreatedDate;

            await _clientRepo.AddAsync(model);
            await _clientRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, ClientInfo model)
        {
            var existing = await _clientRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = model.Name;
            existing.Descritpion = model.Descritpion;
            existing.Email = model.Email;
            existing.Address = model.Address;
            existing.Phone = model.Phone;
            existing.Mobile = model.Mobile;
            existing.Photo = model.Photo;
            existing.Status = model.Status;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _clientRepo.Update(existing);
            await _clientRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _clientRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _clientRepo.Remove(existing);
            await _clientRepo.SaveChangesAsync();
            return true;
        }
    }
}
