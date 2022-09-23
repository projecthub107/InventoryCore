using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class ClientInfo
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Descritpion { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Photo { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}