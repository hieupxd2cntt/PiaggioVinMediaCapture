using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VINMediaCaptureEntities.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public string? TransactionCode { get; set; }

        public int? Type { get; set; }

        public string? Description { get; set; }

        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }
        public string? Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? Status { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public double? Price { get; set; }

        public double? Amount { get; set; }

        public double? Discount { get; set; }
        public int? BranchId { get; set; }

    }

}