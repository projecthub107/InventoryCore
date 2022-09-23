using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Url { get; set; }
        public int? Serial { get; set; }
        public int? Status { get; set; }
        public int ClientId { get; set; }
        public bool? isShow { get; set; }
    }
}