using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Repository.Interface;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepo;

        public QuestionsService(IQuestionsRepository questionsRepo)
        {
            _questionsRepo = questionsRepo;
        }

        public Task<IEnumerable<Questions>> GetAllAsync() =>
            _questionsRepo.GetAllOrderedAsync();
    }
}
