using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int ClientId { get; set; }
    }
}
