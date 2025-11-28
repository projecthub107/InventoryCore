using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ITransactionTypeRepository _typeRepo;

        public TransactionTypeService(ITransactionTypeRepository typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public Task<IEnumerable<TransactionType>> GetByClientAsync(int clientId) =>
            _typeRepo.GetByClientAsync(clientId);
    }
}
