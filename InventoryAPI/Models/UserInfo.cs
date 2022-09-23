using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int ClientId { get; set; }
        public int? QuestionID { get; set; }
        public string Answer { get; set; }
    }
}

