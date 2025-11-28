using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface ITransactionTypeRepository : IGenericRepository<TransactionType>
    {
        Task<IEnumerable<TransactionType>> GetByClientAsync(int clientId);
    }
}
