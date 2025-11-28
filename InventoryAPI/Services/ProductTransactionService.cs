using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly IProductTransactionRepository _transactionRepo;

        public ProductTransactionService(IProductTransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public Task<IEnumerable<ProductTransaction>> GetByClientAsync(int clientId) =>
            _transactionRepo.GetByClientAsync(clientId);

        public Task<IEnumerable<ProductTransaction>> GetByProductAsync(int clientId, int productId) =>
            _transactionRepo.GetByProductAsync(clientId, productId);

        public async Task<ProductTransaction> CreateAsync(ProductTransaction model)
        {
            if (!model.TransactionDate.HasValue)
                model.TransactionDate = DateTime.UtcNow;

            await _transactionRepo.AddAsync(model);
            await _transactionRepo.SaveChangesAsync();
            return model;
        }
    }
}
