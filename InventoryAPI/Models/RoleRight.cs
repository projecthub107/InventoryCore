using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class RoleRight
    {
        [Key]
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int ClientId { get; set; }
    }
}