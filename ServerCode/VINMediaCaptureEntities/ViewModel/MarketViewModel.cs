using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class MarketViewModel
    {
        public Market Search { get; set; }
        public List<Market> Markets { get; set; }
        public MarketViewModel()
        {
            Markets = new List<Market>();
            Search = new Market();
        }
    }
}
