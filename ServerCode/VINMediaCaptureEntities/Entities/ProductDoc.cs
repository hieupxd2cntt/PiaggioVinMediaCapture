using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class ProductDoc
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string VINCode { get; set; }

        public int DocTypeID { get; set; }

        public DateTime DocTypeDate { get; set; }

        public int UserID { get; set; }

        public int? WS_ID { get; set; }
        public string? SerialNumber { get; set; }

        public int ModelID { get; set; }

        public int MarketID { get; set; }

        public int ColorID { get; set; }
        public ProductDoc()
        {
            VINCode = "";
        }

    }
}