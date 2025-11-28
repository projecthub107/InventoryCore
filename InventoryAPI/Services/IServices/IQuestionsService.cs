using InventoryAPI.Models;

namespace InventoryAPI.Services.IServices
{
    public interface IQuestionsService
    {
        Task<IEnumerable<Questions>> GetAllAsync();
    }
}
