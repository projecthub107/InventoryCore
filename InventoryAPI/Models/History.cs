using System.ComponentModel.DataAnnotations;
namespace InventoryAPI.Models
{
    public class History
    {
        [Key]
        public int HistoryId { get; set; }
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSize { get; set; }
        public string ProductWight { get; set; }
        public string ProductColor { get; set; }
        public string ProductSerial { get; set; }
        public int? MinimumQuantityStock { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ProductTransactionId { get; set; }
        public int? TransactionTypeId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? QuantityIn { get; set; }
        public int? QuantityOut { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitSale { get; set; }
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public int? ManufacturerId { get; set; }
        public string ManufacturerNam { get; set; }
        public string HistoryType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ClientId { get; set; }
    }
}