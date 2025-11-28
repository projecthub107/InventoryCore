using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface ITransactionTypeService
    {
        Task<IEnumerable<TransactionType>> GetByClientAsync(int clientId);
    }
}
