using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class Preferences
    {
        [Key]
        public int PreferencesId { get; set; }
        public int? MinimumStock { get; set; }
        public int? CurrencyId { get; set; }
        public int? DefaultLocationId { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int ClientId { get; set; }
    }
}