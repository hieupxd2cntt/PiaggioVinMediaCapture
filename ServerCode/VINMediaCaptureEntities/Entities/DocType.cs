using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class DocType
    {
        [Key]
        public int DocTypeID { get; set; }

        public string? DocTypeCode { get; set; }

        public string? DocTypeName { get; set; }

        public DateTime? FirstUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime? LastReindex { get; set; }

        public DateTime? LastArchive { get; set; }

        public string? ArchivePeriod { get; set; }

        public bool? Disabled { get; set; }

    }
}