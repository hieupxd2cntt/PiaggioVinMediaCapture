using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class DocTypeItemAttr
    {
        [Key]
        public int Id { get; set; }

        public string ItemID { get; set; }

        public string AttrID { get; set; }

        public string? AttrFieldName { get; set; }

        public string AttrDataType { get; set; }

        public int? AttrDataLength { get; set; }

        public string? AttrDescription { get; set; }

        public string? AttrName { get; set; }

        public string? AttrImage { get; set; }

        public bool? isMandatory { get; set; }

        public bool? isManualCollect { get; set; }

        public int? DisplayIDX { get; set; }

        public int? ValidationRuleID { get; set; }

        public int? Disabled { get; set; }

    }
}