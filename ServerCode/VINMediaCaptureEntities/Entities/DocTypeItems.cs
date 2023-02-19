using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class DocTypeItems
    {
        
        public int? DocTypeID { get; set; }
        [Key]
        public int? ItemID { get; set; }

        public string? ItemDescription { get; set; }

        public string? ItemName { get; set; }

        public string? ItemImage { get; set; }

        public bool IsMandatory { get; set; }
        public bool ManualCollect { get; set; }
        public int? ModelID { get; set; }

        public int? MarketID { get; set; }

        public int? ColorID { get; set; }

        public int? DisplayIDX { get; set; }

        public int? Disabled { get; set; }

    }
}