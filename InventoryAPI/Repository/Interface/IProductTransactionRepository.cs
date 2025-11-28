using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IProductTransactionRepository : IGenericRepository<ProductTransaction>
    {
        Task<IEnumerable<ProductTransaction>> GetByClientAsync(int clientId);
        Task<IEnumerable<ProductTransaction>> GetByProductAsync(int clientId, int productId);
    }
}
