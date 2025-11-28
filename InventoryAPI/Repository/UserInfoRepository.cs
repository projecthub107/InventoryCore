using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Repository
{
    public class UserInfoRepository : GenericRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<UserInfo?> GetByUserNameAsync(string userName, int clientId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.ClientId == clientId && u.UserName == userName);
        }

        public async Task<IEnumerable<UserInfo>> GetByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(u => u.ClientId == clientId && (u.Status == 1 || u.Status == null))
                .ToListAsync();
        }
    }
}
