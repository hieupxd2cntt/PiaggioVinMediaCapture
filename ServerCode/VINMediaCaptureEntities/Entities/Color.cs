using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Color
    {
        [Key]
        public int ColorID { get; set; }

        public string ColorCode { get; set; }

        public string ColorName { get; set; }

        public bool? Disable { get; set; }

    }

}