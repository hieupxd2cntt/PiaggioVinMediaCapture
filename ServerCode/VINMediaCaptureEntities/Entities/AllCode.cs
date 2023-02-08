using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class AllCode
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        public int? Value { get; set; }

    }
    public class ALLCODE
    {
        [Key]
        public int ID { get; set; }

        public string CODE { get; set; }

        public string NAME { get; set; }

        public string? DESCRIPTION { get; set; }
        public int? VALUE { get; set; }

    }
}