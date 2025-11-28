using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetActiveByClientAsync(int clientId);
    }
}
