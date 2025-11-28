using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepo;

        public HistoryService(IHistoryRepository historyRepo)
        {
            _historyRepo = historyRepo;
        }

        public Task<IEnumerable<History>> GetByClientAsync(int clientId) =>
            _historyRepo.GetByClientAsync(clientId);

        public Task<IEnumerable<History>> GetByProductAsync(int clientId, int productId) =>
            _historyRepo.GetByProductAsync(clientId, productId);
    }
}
