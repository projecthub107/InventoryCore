using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country?> GetByCodeAsync(string countryCode);
        Task<IEnumerable<Country>> SearchByNameAsync(string namePart);
    }
}
