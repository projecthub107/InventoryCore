using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;



namespace InventoryAPI.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int? clientId = null);
        Task<Product?> GetByIdAsync(int id, int? clientId = null);

        Task<Product> CreateAsync(Product model);
        Task<bool> UpdateAsync(int id, Product model);
        Task<bool> DeleteAsync(int id);
    }
}
