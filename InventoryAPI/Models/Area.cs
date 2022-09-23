using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class Area
    {
        [Key]
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int ClientId { get; set; }
    }
}
