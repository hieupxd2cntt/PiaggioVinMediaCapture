using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class ColorViewModel
    {
        public Color Search { get; set; }
        public List<Color> Colors { get; set; }
        public ColorViewModel()
        {
            Colors = new List<Color>();
            Search = new Color();
        }
    }
}
