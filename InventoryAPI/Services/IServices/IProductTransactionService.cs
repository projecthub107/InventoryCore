using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IProductTransactionService
    {
        Task<IEnumerable<ProductTransaction>> GetByClientAsync(int clientId);
        Task<IEnumerable<ProductTransaction>> GetByProductAsync(int clientId, int productId);
        Task<ProductTransaction> CreateAsync(ProductTransaction model);
    }
}
