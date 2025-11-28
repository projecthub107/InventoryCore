using InventoryAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryAPI.Services.IServices
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAreasAsync(int clientId);
        Task<Area?> GetByIdAsync(int id);
        Task<Area> CreateAsync(Area model);
        Task<bool> UpdateAsync(int id, Area model);
        Task<bool> DeleteAsync(int id);
    }
}
