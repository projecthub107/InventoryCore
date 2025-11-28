using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepo;

        public ProductCategoryService(IProductCategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync(int? clientId = null)
        {
            var all = await _categoryRepo.GetAllAsync();
            if (clientId.HasValue)
                all = all.Where(c => c.ClientId == clientId.Value);
            return all.OrderBy(c => c.CategoryName);
        }

        public Task<ProductCategory?> GetByIdAsync(int id) => _categoryRepo.GetByIdAsync(id);

        public async Task<ProductCategory> CreateAsync(ProductCategory model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.ModifiedDate = now;

            await _categoryRepo.AddAsync(model);
            await _categoryRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, ProductCategory model)
        {
            var existing = await _categoryRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.CategoryName = model.CategoryName;
            existing.CategorySerial = model.CategorySerial;
            existing.Status = model.Status;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _categoryRepo.Update(existing);
            await _categoryRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _categoryRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _categoryRepo.Remove(existing);
            await _categoryRepo.SaveChangesAsync();
            return true;
        }
    }
}
