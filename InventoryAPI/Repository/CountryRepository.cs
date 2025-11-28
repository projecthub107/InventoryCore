using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<Country?> GetByCodeAsync(string countryCode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.CountryCode == countryCode);
        }

        public async Task<IEnumerable<Country>> SearchByNameAsync(string namePart)
        {
            namePart = namePart?.Trim() ?? string.Empty;
            return await _dbSet
                .Where(c => c.CountryName.Contains(namePart))
                .ToListAsync();
        }
    }
}
