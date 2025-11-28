using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int? clientId = null)
        {
            
            var products = await _productRepo.GetAllWithCategoryAsync(clientId);
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id, int? clientId = null)
        {
           
            var products = await _productRepo.GetAllWithCategoryAsync(clientId);
            return products.FirstOrDefault(p => p.ProductId == id);
        }

        public async Task<Product> CreateAsync(Product model)
        {
           
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.ModifiedDate = now;

            await _productRepo.AddAsync(model);
            await _productRepo.SaveChangesAsync();

            return model;
        }

        public async Task<bool> UpdateAsync(int id, Product model)
        {
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null)
                return false;

            // Map updatable fields
            existing.ProductCode = model.ProductCode;
            existing.ProductName = model.ProductName;
            existing.ProductDescription = model.ProductDescription;
            existing.ProductSize = model.ProductSize;
            existing.ProductWight = model.ProductWight;
            existing.ProductColor = model.ProductColor;
            existing.ProductSerial = model.ProductSerial;
            existing.MinimumQuantityStock = model.MinimumQuantityStock;
            existing.Status = model.Status;
            existing.CategoryId = model.CategoryId;
            existing.AreaId = model.AreaId;
            existing.ManufacturerId = model.ManufacturerId;
            existing.Quantity = model.Quantity;
            existing.ImageName = model.ImageName;
            existing.ClientId = model.ClientId; 
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _productRepo.Update(existing);
            await _productRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _productRepo.GetByIdAsync(id);
            if (existing == null)
                return false;

            _productRepo.Remove(existing);
            await _productRepo.SaveChangesAsync();

            return true;
        }
    }
}
