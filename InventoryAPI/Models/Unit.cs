using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int ClientId { get; set; }
        public string Type { get; set; }
    }
}