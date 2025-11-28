using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country?> GetByIdAsync(int id);
        Task<Country?> GetByCodeAsync(string code);
        Task<IEnumerable<Country>> SearchByNameAsync(string namePart);
    }
}
