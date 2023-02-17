using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string LoginName { get; set; }

        public string? SecretKey { get; set; }

        public string? Password { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? PWLastUpdated { get; set; }

        public string? RestoreToken { get; set; }

        public string? RestoreCode { get; set; }

        public DateTime? RestoreExpiry { get; set; }

        public bool? SelfRegistered { get; set; }

        public int? Disabled { get; set; }

    }

}