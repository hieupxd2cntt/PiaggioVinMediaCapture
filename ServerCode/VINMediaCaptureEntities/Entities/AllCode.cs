using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class AllCode
    {
        [Key]
        public int IDX { get; set; }

        public string? CodeName { get; set; }

        public string? TypeName { get; set; }

        public string? CodeVal { get; set; }

        public int? CodeIDX { get; set; }

        public string? Contents { get; set; }

        public int? ResourceID { get; set; }

        public bool? Modify { get; set; }

        public bool? Disable { get; set; }

    }
}