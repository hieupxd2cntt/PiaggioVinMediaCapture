using System.ComponentModel.DataAnnotations;

namespace VINMediaCaptureEntities.Entities
{
    public class Model
    {
        [Key]
        public int ModelID { get; set; }

        public string ModelCode { get; set; }

        public string ModelName { get; set; }

        public int? Disable { get; set; }
        public Model()
        {
            ModelCode = "";
            ModelName = "";
        }

    }
}