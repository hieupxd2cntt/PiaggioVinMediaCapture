using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Market
    {
        [Key]
        public int MarketID { get; set; }

        public string? MarketCode { get; set; }

        public string? Market_Name { get; set; }

        public bool? Disabled { get; set; }

    }
}