using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Logs
    {
        [Key]
        public int LogId { get; set; }

        public string? LogCode { get; set; }

        public int? LogClsID { get; set; }

        public DateTime? Log_Date { get; set; }

        public int? UserID { get; set; }

        public int? WSID { get; set; }

        public int? ObjID { get; set; }

        public string? ObjKeyCode { get; set; }

        public int? EventID { get; set; }

        public string? EventCode { get; set; }

        public string? SWACode { get; set; }

        public long? Disable { get; set; }

    }
}