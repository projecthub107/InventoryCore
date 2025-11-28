using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuRepo;

        public MenuItemService(IMenuItemRepository menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public Task<IEnumerable<MenuItem>> GetMenuAsync(int clientId) =>
            _menuRepo.GetMenuTreeAsync(clientId);

        public Task<MenuItem?> GetByIdAsync(int id) => _menuRepo.GetByIdAsync(id);

        public async Task<MenuItem> CreateAsync(MenuItem model)
        {
            await _menuRepo.AddAsync(model);
            await _menuRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, MenuItem model)
        {
            var existing = await _menuRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Code = model.Code;
            existing.Name = model.Name;
            existing.ParentId = model.ParentId;
            existing.Url = model.Url;
            existing.Serial = model.Serial;
            existing.Status = model.Status;
            existing.isShow = model.isShow;

            _menuRepo.Update(existing);
            await _menuRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _menuRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _menuRepo.Remove(existing);
            await _menuRepo.SaveChangesAsync();
            return true;
        }
    }
}
