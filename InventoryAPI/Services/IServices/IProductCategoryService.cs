using InventoryAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryAPI.Services.IServices
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetCategoriesAsync(int? clientId = null);
        Task<ProductCategory?> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(ProductCategory model);
        Task<bool> UpdateAsync(int id, ProductCategory model);
        Task<bool> DeleteAsync(int id);
    }
}
