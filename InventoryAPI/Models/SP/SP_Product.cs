using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models.SP
{
    public class SP_Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? AreaName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public int ClientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
