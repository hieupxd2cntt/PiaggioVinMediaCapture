using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Drugs
    {
        [Key]
        public int Id { get; set; }

        public string? DrugName { get; set; }

        public int? Type { get; set; }

        public int? UnitId { get; set; }
        public int? Status { get; set; }
        public double? InputPrice { get; set; }
        public double? OutputPrice { get; set; }
        public int? BranchId { get; set; }

    }
}