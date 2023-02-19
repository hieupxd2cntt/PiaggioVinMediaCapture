using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Market
    {
        [Key]
        public int MarketID { get; set; }

        public string? MarketCode { get; set; }

        public string? MarketName { get; set; }

        public int? Disabled { get; set; }

    }
}