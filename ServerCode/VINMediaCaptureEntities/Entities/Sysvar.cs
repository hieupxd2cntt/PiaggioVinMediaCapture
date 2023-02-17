using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class SysVar
    {
        [Key]
        public int ID { get; set; }

        public string VarName { get; set; }

        public string DataType { get; set; }

        public string? VarDesc { get; set; }

        public bool? Modify { get; set; }

        public string? InputMask { get; set; }

        public int? Disabled { get; set; }

    }

}