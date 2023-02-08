using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        public string? PharmacyBranch { get; set; }

        public string? PharmacyBranchName { get; set; }

        public string? Address { get; set; }

        public int? Status { get; set; }

        public string? ContactName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

    }

}