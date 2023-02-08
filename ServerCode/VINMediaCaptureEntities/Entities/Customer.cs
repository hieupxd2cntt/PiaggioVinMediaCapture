using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public int? Type { get; set; }

        public int? Status { get; set; }

    }

}