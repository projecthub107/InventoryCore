using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;
using InventoryAPI.Repository;
using InventoryAPI.Services.IServices;

namespace InventoryAPI.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userRepo;

        public UserInfoService(IUserInfoRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<UserInfo?> GetByIdAsync(int id) => _userRepo.GetByIdAsync(id);

        public Task<UserInfo?> GetByUserNameAsync(string userName, int clientId) =>
            _userRepo.GetByUserNameAsync(userName, clientId);

        public Task<IEnumerable<UserInfo>> GetByClientAsync(int clientId) =>
            _userRepo.GetByClientAsync(clientId);

        public async Task<UserInfo> CreateAsync(UserInfo model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.ModifiedDate = now;

            await _userRepo.AddAsync(model);
            await _userRepo.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(int id, UserInfo model)
        {
            var existing = await _userRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.UserName = model.UserName;
            existing.Password = model.Password;
            existing.Email = model.Email;
            existing.RoleId = model.RoleId;
            existing.Status = model.Status;
            existing.QuestionID = model.QuestionID;
            existing.Answer = model.Answer;
            existing.ModifiedBy = model.ModifiedBy;
            existing.ModifiedDate = DateTime.UtcNow;

            _userRepo.Update(existing);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _userRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _userRepo.Remove(existing);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
