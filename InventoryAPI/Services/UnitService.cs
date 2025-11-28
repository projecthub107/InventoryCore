using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepo;

        public UnitService(IUnitRepository unitRepo)
        {
            _unitRepo = unitRepo;
        }

        public Task<IEnumerable<Unit>> GetByClientAsync(int clientId) =>
            _unitRepo.GetByClientAsync(clientId);
    }
}