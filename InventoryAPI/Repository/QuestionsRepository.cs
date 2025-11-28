using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repository.Interface;

namespace InventoryAPI.Repository
{
    public class QuestionsRepository : GenericRepository<Questions>, IQuestionsRepository
    {
        public QuestionsRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Questions>> GetAllOrderedAsync()
        {
            return await _dbSet
                .OrderBy(q => q.QuestionID)
                .ToListAsync();
        }
    }
}
