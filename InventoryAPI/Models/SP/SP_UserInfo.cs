using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models.SP
{
    public class SP_UserInfo
    {
        [Key]
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

    }
}
