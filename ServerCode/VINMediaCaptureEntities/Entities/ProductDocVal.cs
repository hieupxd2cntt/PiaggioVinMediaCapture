using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class ProductDocVal
    {
        [Key]
        public int Id { get; set; }

        public int ProductDocId { get; set; }

        public string VINCode { get; set; }

        public int DocType_ID { get; set; }

        public int ItemID { get; set; }

        public int AttrID { get; set; }

        public string AttrValue { get; set; }

    }
}