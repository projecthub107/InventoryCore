using InventoryAPI.Models;

namespace InventoryAPI.Repository.Interface
{
    public interface IQuestionsRepository : IGenericRepository<Questions>
    {
        Task<IEnumerable<Questions>> GetAllOrderedAsync();
    }
}
