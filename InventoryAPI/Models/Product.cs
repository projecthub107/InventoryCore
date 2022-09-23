using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
   
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductSize { get; set; }
        public string? ProductWight { get; set; }
        public string? ProductColor { get; set; }
        public string? ProductSerial { get; set; }
        public int? MinimumQuantityStock { get; set; }
        public int? Status { get; set; }
        public int? CategoryId { get; set; }
        public int? AreaId { get; set; }
        public int? ManufacturerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public int ClientId { get; set; }
        public int? Quantity { get; set; }
        public string? ImageName { get; set; }
    }
}
