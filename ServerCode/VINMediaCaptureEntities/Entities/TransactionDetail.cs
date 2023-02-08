using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class TransactionDetail
    {
        [Key]
        public int Id { get; set; }

        public int? TransactionId { get; set; }

        public int? DrugId { get; set; }

        public string? Packages { get; set; }

        public int? UnitId { get; set; }

        public int? WarehouseUnit { get; set; }
        public double? WarehouseQuantity { get; set; }

        public double? Quantity { get; set; }

        public int? Status { get; set; }

        public double? Price { get; set; }
        public double? Discount { get; set; }

        public double? Amount { get; set; }
        public double? PriceId { get; set; }
        public DateTime? ExpireDate { get; set; }

    }

}