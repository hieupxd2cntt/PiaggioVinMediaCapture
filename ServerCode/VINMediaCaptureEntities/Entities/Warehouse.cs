using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        public int DrugId { get; set; }

        public double TotalQuantity { get; set; }

        public double UsedQuantity { get; set; }

        public string? Packages { get; set; }

        public DateTime ExpireDate { get; set; }

        public int Status { get; set; }

        public int BranchId { get; set; }

        public int? Unit { get; set; }

        public int? WarehouseUnit { get; set; }
        public double? PackageQuantity { get; set; }
    }

}