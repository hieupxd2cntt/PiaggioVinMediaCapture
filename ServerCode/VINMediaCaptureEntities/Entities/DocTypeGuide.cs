using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace VINMediaCaptureEntities.Entities
{
    public class DocTypeGuide
    {
        [Key]
        public int Id { get; set; }

        public int ItemID { get; set; }

        public int AttrID { get; set; }

        public int ModelID { get; set; }

        public int MarketID { get; set; }

        public int ColorID { get; set; }

        public int? DocTypeID { get; set; }

        public string? GuideTXT { get; set; }

        public string? GuideImg { get; set; }

        public int? Disabled { get; set; }

    }
}