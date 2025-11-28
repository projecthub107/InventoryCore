using System;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IPreferencesRepository _prefRepo;

        public PreferencesService(IPreferencesRepository prefRepo)
        {
            _prefRepo = prefRepo;
        }

        public Task<Preferences?> GetByClientAsync(int clientId) =>
            _prefRepo.GetByClientAsync(clientId);

        public async Task<Preferences> SaveOrUpdateAsync(Preferences model)
        {
            var existing = await _prefRepo.GetByClientAsync(model.ClientId);

            var now = DateTime.UtcNow;

            if (existing == null)
            {
                model.CreatedDate = now;
                model.ModifiedDate = now;
                await _prefRepo.AddAsync(model);
            }
            else
            {
                existing.MinimumStock = model.MinimumStock;
                existing.CurrencyId = model.CurrencyId;
                existing.DefaultLocationId = model.DefaultLocationId;
                existing.Status = model.Status;
                existing.ModifiedBy = model.ModifiedBy;
                existing.ModifiedDate = now;

                _prefRepo.Update(existing);
                model = existing;
            }

            await _prefRepo.SaveChangesAsync();
            return model;
        }
    }
}
