using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class ProductTransaction
    {
        [Key]
        public int ProductTransactionId { get; set; }
        public int? TransactionTypeId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? QuantityIn { get; set; }
        public int? QuantityOut { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitSale { get; set; }
        public int? LocationId { get; set; }
        public int ClientId { get; set; }
        public string TransactionNumber { get; set; }
    }
}