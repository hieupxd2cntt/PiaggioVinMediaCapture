using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class ModelViewModel
    {
        public VINMediaCaptureEntities.Entities.Model Search { get; set; }
        public List<VINMediaCaptureEntities.Entities.Model> Models { get; set; }
        public ModelViewModel()
        {
            Models = new List<VINMediaCaptureEntities.Entities.Model>();
            Search = new VINMediaCaptureEntities.Entities.Model();
        }
    }
}
