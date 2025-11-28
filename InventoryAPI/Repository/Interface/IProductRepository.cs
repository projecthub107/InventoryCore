using InventoryAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryAPI.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync(int? clientId = null);
    }
}
