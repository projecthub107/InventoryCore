using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepo;

        public CountryService(ICountryRepository countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public Task<IEnumerable<Country>> GetAllAsync() => _countryRepo.GetAllAsync();

        public Task<Country?> GetByIdAsync(int id) => _countryRepo.GetByIdAsync(id);

        public Task<Country?> GetByCodeAsync(string code) => _countryRepo.GetByCodeAsync(code);

        public Task<IEnumerable<Country>> SearchByNameAsync(string namePart) =>
            _countryRepo.SearchByNameAsync(namePart);
    }
}
